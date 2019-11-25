using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public ParticleSystem system;

    Vector3 lastEmit;
    GameObject player;
    GameObject playerModel;
    public float delta = 1;
    public float gap = 0.5f;
    int dir = 1;
    static Footsteps selectedSystem;

    // Start is called before the first frame update
    void OnEnable()
    {
        lastEmit = transform.position;
        selectedSystem = this;
        player = PlayerManager.Instance().players[0];
        playerModel = player.transform.Find("Model").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastEmit, transform.position) > delta)
        {
            selectedSystem = this;
            var pos = transform.position + (playerModel.transform.right * gap * dir);
            dir *= -1;
            ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
            ep.position = pos;
            ep.rotation = playerModel.transform.rotation.eulerAngles.y;
            system.Emit(ep, 1);
            lastEmit = transform.position;
        }
    }
}
