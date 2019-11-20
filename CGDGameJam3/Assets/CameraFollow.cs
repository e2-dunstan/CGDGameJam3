using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    [SerializeField]
    Vector3 offset = new Vector3(0.0f, 7.5f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = player.transform.position;

        transform.position = new Vector3(pos.x + offset.x, pos.y + offset.y, pos.z + offset.z);
        transform.LookAt(pos, transform.up);
    
    }
}
