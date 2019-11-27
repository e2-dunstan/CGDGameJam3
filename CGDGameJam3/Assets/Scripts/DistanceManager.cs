using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public static DistanceManager _instance;
    [HideInInspector] public List<GameObject> lights = new List<GameObject>();
    [HideInInspector] public List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }
    public static DistanceManager Instance()
    {
        if (_instance == null) { _instance = new DistanceManager(); }
        return _instance;
    }
    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Light").Length; i++)
        {
            lights.Add(GameObject.FindGameObjectsWithTag("Light")[i]);
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
        {
            enemies.Add(GameObject.FindGameObjectsWithTag("Enemy")[i]);
        }
    }

    void Update()
    {
    }
    private void LateUpdate()
    {
        
        DisableLightsBasedOnDistance();
    }
    private void DisableLightsBasedOnDistance()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            if (PlayerDistance(15, lights[i].transform.position))
            {
                lights[i].SetActive(false);
            }
            else
            {
                lights[i].SetActive(true);
            }
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            for (int j = 0; j < lights.Count; j++)
            {
                if (EnemyDistance(7, i, lights[j].transform.position))
                {
                    lights[j].SetActive(false);
                }
            }
        }
    }
    public bool PlayerDistance(int _dist, Vector3 _pos)
    {
        return (Vector3.Distance(PlayerManager.Instance().players[0].gameObject.transform.position,
                _pos) > _dist);
    }

    public bool EnemyDistance(int _dist, int _enemy, Vector3 _pos)
    {
        return (Vector3.Distance(enemies[_enemy].transform.position,
                    _pos) < _dist);
    }
}
