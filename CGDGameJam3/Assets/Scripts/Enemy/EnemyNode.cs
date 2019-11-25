using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNode : MonoBehaviour
{
    public EnemyNode[] connectingNodes;
    
    [HideInInspector] public Vector2 position;

    private void Awake()
    {
        position = new Vector2(this.transform.position.x, this.transform.position.z);
    }

    public EnemyNode GetNextRandomNode()
    {
        return connectingNodes[Random.Range(0, connectingNodes.Length)];
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.25f);

        foreach (EnemyNode node in connectingNodes)
        {
            Vector3 heading = node.transform.position - this.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            Debug.DrawRay(transform.position, direction, Color.blue);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.25f);
    }
#endif

}
