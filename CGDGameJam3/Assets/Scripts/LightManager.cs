using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Light[] sceneLights;
    public float lightIntensity = 1.0f;
    public float shutoffDuration = 1.0f;
    public Color startColour;
    public Color endColour;

    void Start()
    {
        foreach (var light in sceneLights)
        {
            light.intensity = lightIntensity;
        }
    }

    public void SetSceneLightsIntensity(float value)
    {
        foreach (var light in sceneLights)
        {
            light.intensity = value;
        }
    }

    public void SetSceneLightsColourLerp(float value)
    {
        foreach (var light in sceneLights)
        {
            light.color = Color.Lerp(endColour, startColour, value);
        }
    }

    IEnumerator DarknessThenRelight(float percentageDim)
    {
        SetSceneLightsIntensity(0);
        yield return new WaitForSeconds(shutoffDuration);
        SetSceneLightsColourLerp(percentageDim);
        SetSceneLightsIntensity(lightIntensity * percentageDim);
    }


    [ContextMenu("Trigger to 0")]
    void TestTrigger0()
    {
        StartCoroutine(DarknessThenRelight(0));
    }

    [ContextMenu("Trigger to 25")]
    void TestTrigger25()
    {
        StartCoroutine(DarknessThenRelight(0.25f));
    }

    [ContextMenu("Trigger to 50")]
    void TestTrigger50()
    {
        StartCoroutine(DarknessThenRelight(0.5f));
    }

    [ContextMenu("Trigger to 75")]
    void TestTrigger75()
    {
        StartCoroutine(DarknessThenRelight(0.75f));
    }

    [ContextMenu("Trigger to 100")]
    void TestTrigger100()
    {
        StartCoroutine(DarknessThenRelight(1.0f));
    }
}
