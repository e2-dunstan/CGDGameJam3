using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    enum EnemyState
    {
        FollowNodes,
        Chase
    };

    EnemyState currentState = EnemyState.FollowNodes;

    [Header("Model Attributes")]
    public Transform model;
    public float rotationDamping = 10;

    [Header("Enemy Attributes")]
    public float speed = 2;
    public float chaseSpeed = 4;
    public float chaseRange = 5;
    public float nodeRange = 0.1f;

    Rigidbody enemyRigidbody;

    Transform playerTransform;

    Vector2 playerPosition;
    Vector2 enemyPosition;
    Vector3 previousPosition;

    EnemyNode currentNode;

    // Start is called before the first frame update
    private void Start()
    {
        enemyRigidbody = this.GetComponent<Rigidbody>();

        currentNode = EnemyManager.instance.FindNearestNode(this.transform);

        previousPosition = this.transform.position;
        
        playerTransform = EnemyManager.instance.GetPlayerTransform();
    }

    private void FixedUpdate()
    {
        enemyPosition.Set(transform.position.x, transform.position.z);
        playerPosition.Set(playerTransform.position.x, playerTransform.position.z);

        if (PlayerInRange())
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.FollowNodes;
        }

        switch (currentState)
        {
            case EnemyState.FollowNodes:
                FollowNodesState();
                break;
                
            case EnemyState.Chase:
                ChaseState();
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
            EnemyNode newNode = currentNode.GetNextRandomNode();

            if(newNode == null)
            {
                Debug.LogError("Connecting Nodes missing from EnemyNode");
            }
            else
            {
                currentNode = newNode;
            }
        }

        MoveTowardsNode();
    }

    private void ChaseState()
    {
        MoveTowardsPlayer();
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

        return distance < chaseRange;
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
        Vector3 localDirection = transform.InverseTransformDirection(direction);
        previousPosition = transform.position;

        Quaternion dersiredRotation = Quaternion.LookRotation(new Vector3(localDirection.x, 0f, localDirection.z));
        model.localRotation = Quaternion.Lerp(model.localRotation, dersiredRotation, Time.deltaTime * rotationDamping);
    }
}

