using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;

    public EnemyNode[] allNodes;
    public GameObject player;

    public List<EnemyBehaviour> enemies;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        GameObject[] gameObjectNodes = GameObject.FindGameObjectsWithTag("EnemyNode");
        allNodes = new EnemyNode[gameObjectNodes.Length];

        for (int i = 0; i < gameObjectNodes.Length; i++)
        {
            allNodes[i] = gameObjectNodes[i].GetComponent<EnemyNode>();
        }
    }

    public EnemyNode FindNearestNode(Transform enemy)
    {
        Vector2 enemyPosition = new Vector2(enemy.position.x, enemy.position.z);
        float shortestDistance = Mathf.Infinity;
        EnemyNode nearestNode = new EnemyNode();

        foreach (EnemyNode node in allNodes)
        {
            float distance = Vector2.Distance(node.position, enemyPosition);

            if (distance < shortestDistance && NodeInSight(enemy, node.transform))
            {
                shortestDistance = distance;

                nearestNode = node;
            }
        }

        return nearestNode;
    }

    public bool NodeInSight(Transform enemy, Transform node)
    {
        RaycastHit hit;

        Vector3 enemySight = enemy.position + Vector3.up;
        Vector3 nodeSight = node.position + Vector3.up;

        if (Physics.Linecast(enemySight, nodeSight, out hit))
        {
            if (hit.transform.CompareTag("Wall"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return true;
    }

    public bool IsEnemyChasingPlayer()
    {
        foreach (EnemyBehaviour enemy in enemies)
        {
            if(enemy.IsChasing())
            {
                return true;
            }
        }

        return false;
    }

    public void AddEnemyToScene(EnemyBehaviour enemy)
    {
        enemies.Add(enemy);
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
}
