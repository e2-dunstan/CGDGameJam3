using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject player;
    [HideInInspector] public Vector3 offset = new Vector3(0.0f, 7.5f, 0.0f);
    [HideInInspector] public Vector3 lookAtPosOffset = new Vector3(0.0f, 0.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerMovement>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = player.transform.position;

        transform.position = new Vector3(pos.x + offset.x, pos.y + offset.y, pos.z + offset.z);
        pos = new Vector3(pos.x + offset.x, pos.y, pos.z + offset.z);
        //pos = new Vector3(pos.x + lookAtPosOffset.x, pos.y + lookAtPosOffset.y, pos.z + lookAtPosOffset.z);
        transform.LookAt(pos, transform.up);
    }
}
