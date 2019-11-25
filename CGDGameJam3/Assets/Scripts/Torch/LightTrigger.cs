using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTrigger : MonoBehaviour
{
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
    }

    public void LightIsFixatedUpon(float lightIntensity)
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
        }
    }
}
