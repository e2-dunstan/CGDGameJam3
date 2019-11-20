using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;

    public EnemyNode[] allNodes;
    public GameObject player;

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

            if (distance < shortestDistance)
            {
                shortestDistance = distance;

                nearestNode = node;
            }
        }

        return nearestNode;
    }

    public Transform GetPlayerTransform()
    {
        return player.transform;
    }
}
