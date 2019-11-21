using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchLights : MonoBehaviour
{

    public Transform [] potentialPositions;
    List<bool> positionsTaken = new List<bool>();
    public GameObject[] lights;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < potentialPositions.Length; ++i)
        {
            positionsTaken.Add(false);
        }

        for (int i = 0; i < lights.Length; ++i)
        {
            int ranPos = Random.Range(0, potentialPositions.Length);

            while (positionsTaken[ranPos] == true)
            {
                ranPos = Random.Range(0, potentialPositions.Length);
            }

            lights[i].transform.position = potentialPositions[ranPos].transform.position;
            positionsTaken[ranPos] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < lights.Length; ++i)
        {
            lights[i].GetComponent<Light>().intensity -= Time.deltaTime * 0.01f;
        }
    }
}
