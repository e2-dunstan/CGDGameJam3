using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    ParticleSystem heartEffect;
    ParticleSystem.EmissionModule heartSpeed;
    // Start is called before the first frame update
    void Start()
    {
        heartEffect = GetComponentInChildren<ParticleSystem>();
        heartSpeed = heartEffect.emission;
    }

    // Update is called once per frame
    void Update()
    {
        heartSpeed.rateOverTime = 1.5f;

        for (int i = 0; i < DistanceManager.Instance().enemies.Count; i++)
        {
            if (DistanceManager.Instance().EnemyDistance(10, i, PlayerManager.Instance().players[0].transform.position))
            {
                heartSpeed.rateOverTime = 3;
            }
            if (DistanceManager.Instance().enemies[i].GetComponent<EnemyBehaviour>().IsChasing())
            {
                heartSpeed.rateOverTime = 6;
            }
        }
    }
}
