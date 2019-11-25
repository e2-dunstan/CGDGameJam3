using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Footsteps : MonoBehaviour
{
    public ParticleSystem system;

    Vector3 lastEmit;
    GameObject player;
    public float delta = 1;
    public float gap = 0.5f;
    int dir = 1;
    static Footsteps selectedSystem;

    // Start is called before the first frame update
    void Start()
    {
        lastEmit = transform.position;
        selectedSystem = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(lastEmit, transform.position) > delta)
        {
            selectedSystem = this;
            var pos = player.transform.position + (player.transform.right * gap * dir);
            dir *= -1;
            ParticleSystem.EmitParams ep = new ParticleSystem.EmitParams();
            ep.position = pos;
            ep.rotation = player.transform.rotation.eulerAngles.y;
            system.Emit(ep, 1);
            lastEmit = transform.position;
        }
    }
}
