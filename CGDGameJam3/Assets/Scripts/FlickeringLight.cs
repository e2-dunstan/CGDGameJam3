using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    private Light lightComponent;
    private float baseIntensity;

    public float amount = 0.5f;
    public float speed = 1.0f;

    private float timeElapsed = 0;
    private float timeBetweenFlickers = 0.1f;

    private void Start()
    {
        lightComponent = GetComponent<Light>();
        baseIntensity = lightComponent.intensity;
    }

    private void Update()
    {
        if (timeElapsed >= timeBetweenFlickers)
        {
            StartCoroutine(AdjustIntensity());
            timeElapsed = 0;
            timeBetweenFlickers = Random.Range(0.05f, 0.1f);
        }
        else
        {
            timeElapsed += Time.deltaTime * speed;
        }
    }

    private IEnumerator AdjustIntensity()
    {
        float newIntensity = baseIntensity + Random.Range(-amount, amount);
        float currentIntensity = lightComponent.intensity;
        float diff = newIntensity - currentIntensity;

        for(float t = 0.0001f; t < 1; t += Time.deltaTime * 10f)
        {
            lightComponent.intensity = currentIntensity + (diff * t / 1.0f);
            yield return null;
        }
    }
}