using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    [HideInInspector] int numberOfPlayers = 1;
    public GameObject playerPrefab;

    public List<GameObject> players = new List<GameObject>(); //Made public for sol's script!

    public GameObject spawnPosParent;

    static PlayerManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        InitialisePlayer();
    }

    public static PlayerManager Instance()
    {
        if (instance == null) { instance = new PlayerManager(); }
        return instance;
    }

    // Start is called before the first frame update
    void InitialisePlayer()
    {
        List<Transform> spawnPoints = new List<Transform>(spawnPosParent.GetComponentsInChildren<Transform>());

        spawnPoints.RemoveAt(0);
        for (int i = 0; i < numberOfPlayers; ++i)
        {
            var player = Instantiate(playerPrefab, spawnPoints[i].position, Quaternion.identity);

            players.Add(player);
            players[i].GetComponent<PlayerMovement>().playerNum = i + 1;
            players[i].gameObject.name = "Player" + (i + 1).ToString();
            if (numberOfPlayers > 1)
            {
                if (i == 0)
                {
                    players[i].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.0f, 0.0f), new Vector2(0.5f, 1.0f));
                }
                else if (i == 1)
                {
                    players[i].GetComponentInChildren<Camera>().rect = new Rect(new Vector2(0.5f, 0.0f), new Vector2(0.5f, 1.0f));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerCount(int amount)
    {
        numberOfPlayers = amount;
    }
}
