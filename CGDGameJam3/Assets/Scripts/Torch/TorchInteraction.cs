using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteraction : MonoBehaviour
{
    TorchStatus torchStatus;

    // Start is called before the first frame update
    void Start()
    {
        torchStatus = GetComponentInParent<TorchStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if(hit.transform.CompareTag("LightTrigger"))
            {
                hit.transform.GetComponent<LightTrigger>().LightIsFixatedUpon(torchStatus.GetCurrentIntensity());
            }
        }

        foreach (EnemyBehaviour enemy in EnemyManager.instance.enemies)
        {
            if (enemy.PlayerInSight())
            {
                if (EnemyInSight(enemy.transform))
                {
                    enemy.aggravateEnemy = true;
                }
                else
                {
                    enemy.aggravateEnemy = false;
                }
            }
            else
            {
                enemy.aggravateEnemy = false;
            }
        }
    }

    bool EnemyInSight(Transform enemyTransform)
    {
        Vector3 enemyDirection = enemyTransform.position - this.transform.position;
        float angle = Vector3.Angle(enemyDirection, this.transform.forward);

        if (angle < torchStatus.flashLight.spotAngle * 0.5f)
        {
            return true;
        }

        return false;
    }
}
