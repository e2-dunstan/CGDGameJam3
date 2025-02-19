﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    enum EnemyState
    {
        FollowNodes,
        Chase,
        LookAround
    };

    EnemyState currentState = EnemyState.FollowNodes;
    EnemyState previousState = EnemyState.FollowNodes;

    [Header("Model Attributes")]
    public Transform model;
    public float rotationDamping = 10;
    public bool changeNodeIfStuck = false;
    public float idleTime;

    [Header("Enemy Attributes")]

    [FMODUnity.EventRef]
    public string lookingSound = "";
    [FMODUnity.EventRef]
    public string chasingSound = "";
    [FMODUnity.EventRef]
    public string footstepSound = "";
    [FMODUnity.EventRef]
    public string killingSound = "";

    public float speed = 2;
    public float chaseSpeed = 4;
    public float dectectionRange = 3;
    public float chaseRange = 5;
    public float nodeRange = 0.1f;
    public float lineOfSightTime = 2f;
    public bool debug = false;

    [HideInInspector] 
    public bool aggravateEnemy = false;

    Rigidbody enemyRigidbody;
    Animator enemyAnimator;

    Transform playerTransform;

    Vector2 playerPosition;
    Vector2 enemyPosition;
    Vector3 previousPosition;

    float stuckTimer = 0;
    float idleTimer = 0;

    float scaleRange = 1.25f;

    float lineOfSightTimer = 0;

    bool freezeModelRotation = false;
    bool followPlayer = false;

    bool canGrowl = true;
    float minTimeBetweenGrowl = 5f;
    float maxTimeBetweenGrowl = 10f;

    public EnemyNode currentNode;

    // Start is called before the first frame update
    private void Start()
    {
        enemyRigidbody = this.GetComponent<Rigidbody>();
        enemyRigidbody.freezeRotation = true;

        enemyAnimator = this.GetComponentInChildren<Animator>();

        currentNode = EnemyManager.instance.FindNearestNode(this.transform);

        EnemyManager.instance.AddEnemyToScene(this);

        previousPosition = this.transform.position;

        playerTransform = PlayerManager.Instance().players[0].transform;

        VFXManager.Instance().CreateParticleSystemForObject(VFXManager.Instance().enemyWalkPS, VFXManager.Instance().enemyWalkPSList);
    }

    private void FixedUpdate()
    {
        if(previousState != currentState)
        {
            switch (currentState)
            {
                case EnemyState.Chase:
                    FMODUnity.RuntimeManager.PlayOneShot(chasingSound, this.transform.position);
                    break;
            }

            previousState = currentState;
        }

        enemyRigidbody.velocity = Vector3.zero;

        enemyPosition.Set(transform.position.x, transform.position.z);
        playerPosition.Set(playerTransform.position.x, playerTransform.position.z);

        if (PlayerInRange() || aggravateEnemy)
        {
            ExitLookAroundState();

            currentState = EnemyState.Chase;
        }

        switch (currentState)
        {
            case EnemyState.FollowNodes:
                FollowNodesState();
                break;
                
            case EnemyState.Chase:
                ChaseState();
                break;

            case EnemyState.LookAround:
                LookAroundState();
                break;
        }

        RotateToMovement();

        if(canGrowl)
        {
            PlayGrowl();
        }
    }
    private void PlayGrowl()
    {
        float growlTime = Random.Range(minTimeBetweenGrowl, maxTimeBetweenGrowl);

        switch (currentState)
        {
            case EnemyState.FollowNodes:
            case EnemyState.LookAround:
                FMODUnity.RuntimeManager.PlayOneShot(lookingSound, this.transform.position);
                break;

            case EnemyState.Chase:
                FMODUnity.RuntimeManager.PlayOneShot(chasingSound, this.transform.position);
                growlTime *= 0.5f;
                break;
        }

        StartCoroutine(randomTime(growlTime));
    }

    IEnumerator randomTime(float time)
    {
        canGrowl = false;
        yield return new WaitForSeconds(time);
        canGrowl = true;
    }

    private void FollowNodesState()
    {
        if (currentNode == null)
        {
            currentNode = EnemyManager.instance.FindNearestNode(this.transform);
        }

        if (NodeInRange())
        {
            ChangeNode();
        }

        MoveTowardsNode();
    }

    private void ChangeNode()
    {
        EnemyNode newNode = currentNode.GetNextRandomNode();

        if (newNode == null)
        {
            Debug.LogError("Connecting Nodes missing from EnemyNode");
        }
        else
        {
            currentNode = newNode;
        }
    }

    private void ChaseState()
    {
        if (!PlayerInRange() && !aggravateEnemy)
        {
            EnterLookAroundState();
            return;
        }

        MoveTowardsPlayer();
    }

    private void EnterLookAroundState()
    {
        idleTimer = 0f;
        enemyAnimator.SetBool("LookAround", true);
        freezeModelRotation = true;
        currentState = EnemyState.LookAround;
    }

    private void LookAroundState()
    {
        if (idleTimer < idleTime)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer >= idleTime)
            {
                ExitLookAroundState();
            }
        }
    }

    private void ExitLookAroundState()
    {
        idleTimer = 0f;
        enemyAnimator.SetBool("LookAround", false);
        freezeModelRotation = false;
        currentNode = EnemyManager.instance.FindNearestNode(this.transform);
        currentState = EnemyState.FollowNodes;
    }

    private bool NodeInRange()
    {
        Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.z);

        float distance = Vector2.Distance(currentNode.position, enemyPosition);

        return distance < nodeRange;
    }

    private bool PlayerInRange()
    {
        float distance = Vector2.Distance(playerPosition, enemyPosition);

        //When in chase state, use chase range instead of dectection range

        bool playerSprinting = InputHandler.Instance().GetSprintHold();

        float range = currentState == EnemyState.Chase ? chaseRange : dectectionRange;

        bool inRange = distance < range * (playerSprinting ? scaleRange : 1.0f);

        if (inRange)
        {
            if(PlayerInSight())
            {
                followPlayer = true;
                lineOfSightTimer = 0;
            }
            else
            {
                if (followPlayer)
                {
                    if (lineOfSightTimer < lineOfSightTime)
                    {
                        lineOfSightTimer += Time.deltaTime;
                        if (lineOfSightTimer >= lineOfSightTime)
                        {
                            followPlayer = false;
                        }
                    }
                }
            }
        }

        return followPlayer;
    }

    public bool PlayerInSight()
    {
        RaycastHit hit;

        Vector3 playerSight = playerTransform.position + Vector3.up;
        Vector3 enemySight = this.transform.position + Vector3.up;

        if (Physics.Linecast(enemySight, playerSight, out hit))
        {
            if(hit.transform == playerTransform)
            {
                Debug.DrawLine(enemySight, playerSight, Color.green);
                return true;
            }
            else
            {
                Debug.DrawLine(enemySight, playerSight, Color.red);
                return false;
            }
        }

        return false;
    }

    public void MoveTowardsNode()
    {
        Vector3 moveVector = (currentNode.transform.position - this.transform.position);
        moveVector.y = 0;

        moveVector.Normalize();

        enemyRigidbody.MovePosition(this.transform.position + moveVector * speed * Time.deltaTime);
        VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, VFXManager.Instance().enemyWalkPSList, new Vector3(0, 0.25f, 0));
    }

    public void MoveTowardsPlayer()
    {
        Vector2 moveVector2D = (playerPosition - enemyPosition);
        Vector3 moveVector = new Vector3(moveVector2D.x, 0 , moveVector2D.y);

        moveVector.Normalize();

        enemyRigidbody.MovePosition(this.transform.position + moveVector * chaseSpeed * Time.deltaTime);
        VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, VFXManager.Instance().enemyWalkPSList, new Vector3(0, 0.5f, 0));

    }

    private void RotateToMovement()
    {
        Vector3 direction = this.transform.position - previousPosition;
        float velocity = direction.magnitude / Time.fixedDeltaTime;

        enemyAnimator.SetFloat("Speed", velocity);

        /*
        if (velocity < 1.9f)
        {
            stuckTime += Time.fixedDeltaTime;

            if (stuckTime > 0.5f)
            {
                currentNode = EnemyManager.instance.FindNearestNode(this.transform);
                ChangeNode();
            }
        }
        else
        {
            stuckTime = 0f;
        }
        */

        if (freezeModelRotation) return;

        Vector3 localDirection = transform.InverseTransformDirection(direction);
        previousPosition = transform.position;

        Quaternion dersiredRotation = Quaternion.LookRotation(new Vector3(localDirection.x, 0f, localDirection.z));
        model.localRotation = Quaternion.Lerp(model.localRotation, dersiredRotation, Time.deltaTime * rotationDamping);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            FMODUnity.RuntimeManager.PlayOneShot(killingSound, this.transform.position);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(changeNodeIfStuck)
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                stuckTimer += Time.fixedDeltaTime;

                if (stuckTimer > 0.5f)
                {
                    stuckTimer = 0;
                    currentNode = EnemyManager.instance.FindNearestNode(this.transform);

                    ChangeNode();
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        stuckTimer = 0;
    }

    public bool IsChasing()
    {
        return currentState == EnemyState.Chase;
    }
}

