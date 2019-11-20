using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = player.transform.position;

        transform.position = new Vector3(pos.x, pos.y + 10.0f, pos.z);
        transform.LookAt(pos, transform.up);
    
    }
}
