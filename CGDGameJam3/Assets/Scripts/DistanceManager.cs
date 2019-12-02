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
        
    }
   
    public bool PlayerDistance(int _dist, Vector3 _pos)
    {
        if (PlayerManager.Instance().players.Count < 0)
        {
            return (Vector3.Distance(PlayerManager.Instance().players[0].gameObject.transform.position,
            _pos) > _dist);
        }
        else
        {
            return (false);
        }
    }

    public bool EnemyDistance(int _dist, int _enemy, Vector3 _pos)
    {
        return (Vector3.Distance(enemies[_enemy].transform.position,
                    _pos) < _dist);
    }
}
