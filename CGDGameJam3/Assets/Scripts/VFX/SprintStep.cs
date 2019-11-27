using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintStep : MonoBehaviour
{
    public ParticleSystem system;

    Vector3 lastEmit;
    GameObject player;
    GameObject playerModel;
    public float delta = 1;
    public float gap = 0.5f;
    int dir = 1;

    void OnEnable()
    { 
        player = PlayerManager.Instance().players[0];
        playerModel = player.GetComponent<PlayerMovement>().model;
        lastEmit = player.transform.position;
    }    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (InputHandler.Instance().GetSprintHold())
        {
            if (Vector3.Distance(lastEmit, player.transform.position) > delta)
            {
                var pos = transform.position + (playerModel.transform.right * gap * dir);
                dir *= -1;
                ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
                ep.position = pos;
                ep.rotation = playerModel.transform.rotation.eulerAngles.y;
                system.Emit(ep, 1);
                lastEmit = player.transform.position;
            }
        }
        else
        {
            lastEmit = player.transform.position;
        }
    }
}