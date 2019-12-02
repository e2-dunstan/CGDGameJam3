using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TorchStatus : MonoBehaviour
{
    public Light flashLight;

    private bool isLightOn = true;

    public float flashyTime = 3.0f;
    private bool isFlashing = false;

    public float dt = 0.0f;

    public float minLightAngle = 35;
    public float maxLightAngle = 75;

    public float maxLightRange = 12;
    public float minLightRange = 6;

    public float minLightIntensity = 2;
    public float maxLightIntensity = 10;

    public GameObject lightStart;
    public GameObject lightEnd;

    private bool isFlickering = false;

    // Start is called before the first frame update
    void Start()
    {
        flashLight.intensity = maxLightIntensity;
        flashLight.range = minLightRange;
        flashLight.spotAngle = minLightAngle;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scroll = Input.mouseScrollDelta;

        //Debug.Log(scroll);
        float scrollAmount = Mathf.Clamp(scroll.y, -1.0f, 1.0f);

        if (flashLight.spotAngle + scrollAmount > minLightAngle && (flashLight.spotAngle + scrollAmount) < maxLightAngle)
        {
            flashLight.spotAngle += scrollAmount;

            if (flashLight.range + scrollAmount < maxLightRange && flashLight.range + scrollAmount > minLightRange)
            {
                flashLight.range += scrollAmount;
            }

            if (flashLight.intensity - scrollAmount > minLightIntensity && flashLight.intensity - scrollAmount < maxLightIntensity)
            {
                flashLight.intensity -= scrollAmount;
            }
        }
       
        if (Input.GetKeyDown("p"))
        {
            if(!isFlashing)
            {
                promptLightFlicker();
            }
        }
    }

    public void promptLightFlicker()
    {
        if (!isFlickering)
        {
            isFlickering = true;
            StartCoroutine(flickerLight());
        }
    }

    IEnumerator flickerLight()
    {
        isFlashing = true;
        bool flashingIsOver = false;
        float startingFlashyTime = 0.1f;
        List<float> timeBetweenFlashes = new List<float>(new float[] {0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.2f, 0.2f, 0.3f, 0.3f});
       float dt2 = 0.0f;
       float dt3 = 0.0f;
       float flashDuration = 3.0f;

        while (flashingIsOver != true)
        {

            if (dt2 > startingFlashyTime)
            {
                if (isLightOn)
                {
                    isLightOn = false;
                    flashLight.enabled = false;
                    Debug.Log("turning light off");
                    dt2 = 0.0f;
                }
                else
                {
                    isLightOn = true;
                    flashLight.enabled = true;
                    Debug.Log("turning light on");
                    dt2 = 0.0f;
                }

                int randomIndex = Random.Range(0, timeBetweenFlashes.Count);
                startingFlashyTime = timeBetweenFlashes[randomIndex];
                timeBetweenFlashes.RemoveAt(randomIndex);
            }

            if(dt3 > flashDuration || timeBetweenFlashes.Count <= 0)
            {
                Debug.Log("Flash over");
                isFlickering = false;
                isFlashing = false;
                isLightOn = true;
                flashLight.enabled = true;
                flashingIsOver = true;
            }

            dt2 += Time.deltaTime;
            dt3 += Time.deltaTime;

            yield return null;
        }

        yield return 0;
    }

    public float GetCurrentIntensity()
    {
        return flashLight.intensity;
    }
}
