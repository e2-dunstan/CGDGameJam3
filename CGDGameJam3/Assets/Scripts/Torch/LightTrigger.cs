using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
    public GameObject particleSystemObj;
    public ParticleSystem particleSystem;
    private ParticleSystem.EmissionModule emission;

    private float currentIntensity = 0.0f;

    public float decreaseProgressSpeed = 5.0f;

    public float maxIntensity = 10.0f;

    public bool hasBeenCompleted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (particleSystem != null)
        {
            emission = particleSystem.emission;
            emission.rateOverTime = 0.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!hasBeenCompleted && particleSystem != null)
        {
            if (currentIntensity - (decreaseProgressSpeed * Time.deltaTime) > 0.0f)
            {
                currentIntensity = currentIntensity - (decreaseProgressSpeed * Time.deltaTime);
                emission.rateOverTime = currentIntensity;
            }
        }

        emission.rateOverTime = 0.0f;
    }

    public void LightIsFixatedUpon(float lightIntensity, GameObject incomingTargetPos)
    {
        if (particleSystem != null)
        {
            if (currentIntensity + (lightIntensity / 10) < maxIntensity)
            {
                currentIntensity += lightIntensity / 10;
                emission.rateOverTime = currentIntensity + (lightIntensity / 10);
            }
            else
            {
                hasBeenCompleted = true;

            }

            GameObject targetPosition = incomingTargetPos.GetComponent<TorchInteraction>().GetTorchSpawnPoint();

            //particleSystem.transform.position = transform.position;

            //Vector3 relativePos = particleSystem.transform.position - incomingTargetPos.transform.position;

            //// the second argument, upwards, defaults to Vector3.up
            //Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            //particleSystem.transform.rotation = rotation;

            particleSystem.transform.position = targetPosition.transform.position;

            Vector3 relativePos = particleSystem.transform.position - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.down);
            particleSystem.transform.rotation = rotation;
        }
    }
}
