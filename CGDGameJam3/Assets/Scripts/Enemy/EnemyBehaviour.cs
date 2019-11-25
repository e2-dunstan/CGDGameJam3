using System.Collections;
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

    [Header("Model Attributes")]
    public Transform model;
    public float rotationDamping = 10;
    public bool changeNodeIfStuck = false;
    public float idleTime;
    

    [Header("Enemy Attributes")]
    public float speed = 2;
    public float chaseSpeed = 4;
    public float dectectionRange = 3;
    public float chaseRange = 5;
    public float nodeRange = 0.1f;

    Rigidbody enemyRigidbody;
    Animator enemyAnimator;

    Transform playerTransform;

    Vector2 playerPosition;
    Vector2 enemyPosition;
    Vector3 previousPosition;

    float stuckTimer;
    float idleTimer;

    bool freezeModelRotation = false;


    EnemyNode currentNode;

    // Start is called before the first frame update
    private void Start()
    {
        enemyRigidbody = this.GetComponent<Rigidbody>();
        enemyRigidbody.freezeRotation = true;

        enemyAnimator = this.GetComponentInChildren<Animator>();

        currentNode = EnemyManager.instance.FindNearestNode(this.transform);

        previousPosition = this.transform.position;
        
        playerTransform = EnemyManager.instance.GetPlayerTransform();
    }

    private void FixedUpdate()
    {
        enemyRigidbody.velocity = Vector3.zero;

        enemyPosition.Set(transform.position.x, transform.position.z);
        playerPosition.Set(playerTransform.position.x, playerTransform.position.z);

        if (PlayerInRange())
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
        if (!PlayerInRange())
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
        return distance < (currentState == EnemyState.Chase ? chaseRange : dectectionRange);
    }

    public void MoveTowardsNode()
    {
        Vector3 moveVector = (currentNode.transform.position - this.transform.position);
        moveVector.y = 0;

        moveVector.Normalize();

        enemyRigidbody.MovePosition(this.transform.position + moveVector * speed * Time.deltaTime);
    }

    public void MoveTowardsPlayer()
    {
        Vector2 moveVector2D = (playerPosition - enemyPosition);
        Vector3 moveVector = new Vector3(moveVector2D.x, 0 , moveVector2D.y);

        moveVector.Normalize();

        enemyRigidbody.MovePosition(this.transform.position + moveVector * chaseSpeed * Time.deltaTime);
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
}

