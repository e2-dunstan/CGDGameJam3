using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public ParticleSystem system;

    [FMODUnity.EventRef]
    public string selectSound;
    FMOD.Studio.EventInstance footstepEvent;
    Vector3 lastEmit;
    GameObject player;
    GameObject playerModel;
    public float delta = 1;
    public float gap = -0.5f;
    int dir = 1;
    int footstepVal;
    private void Start()
    {
    }
    void OnEnable()
    {
        player = PlayerManager.Instance().players[0];
        playerModel = player.GetComponent<PlayerMovement>().model;
        lastEmit = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (!InputHandler.Instance().GetSprintHold())
        {
            footstepVal = Random.Range(1, 4);
            footstepEvent.setParameterValue("Surface", footstepVal);

            if (Vector3.Distance(lastEmit, player.transform.position) > delta)
            {
                var pos = transform.position + (playerModel.transform.right * gap * dir);
                dir *= -1;
                ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
                ep.position = pos;
                ep.rotation = playerModel.transform.rotation.eulerAngles.y;
                system.Emit(ep, 1);
                lastEmit = player.transform.position;

                footstepEvent = FMODUnity.RuntimeManager.CreateInstance(selectSound);
                footstepEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(player.gameObject));
                footstepEvent.start();
            }
        }
        else
        {
            lastEmit = player.transform.position;
        }
    }

}
