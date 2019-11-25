using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTriggerKeySpawn : MonoBehaviour
{
    LightTrigger lightTrigger;
    GameObject key;

    public bool hasEventEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        lightTrigger = gameObject.GetComponentInParent<LightTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if(lightTrigger.hasBeenCompleted && !hasEventEnded)
        {
            if (key != null)
            {
                key.SetActive(true);
            }
            else
            {
                Debug.Log("Light trigger tried to spawn a key, but no key was attached to the gameobject");
            }

            hasEventEnded = true;
        }
    }
}
