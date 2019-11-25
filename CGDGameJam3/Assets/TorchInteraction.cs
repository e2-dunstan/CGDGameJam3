using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteraction : MonoBehaviour
{
    TorchStatus torchStatus;

    // Start is called before the first frame update
    void Start()
    {
        torchStatus = GetComponentInParent<TorchStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), transform.TransformDirection(Vector3.forward), out hit))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            if(hit.transform.gameObject.CompareTag("LightTrigger"))
            {
                hit.transform.gameObject.GetComponent<LightTrigger>().LightIsFixatedUpon(torchStatus.GetCurrentIntensity());
                Debug.Log("Hit light trigger");
            }

        }
    }


}
