using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TorchStatus : MonoBehaviour
{
    [SerializeField] private Light flashLight;

    private bool isLightOn = true;

    public float flashyTime = 3.0f;
    private bool isFlashing = false;

    public float dt = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scroll = Input.mouseScrollDelta;
        //Debug.Log(scroll);
        

        if ((flashLight.spotAngle + (scroll.y / 2)) > 35 && (flashLight.spotAngle + (scroll.y / 2)) < 74)
        {
            flashLight.spotAngle += scroll.y;

            if (flashLight.range + (scroll.y / 2) < 12 && flashLight.range + (scroll.y / 2) > 6)
            {
                flashLight.range += (scroll.y / 2);
            }

            if (flashLight.intensity - (scroll.y) > 2 && flashLight.intensity - scroll.y < 10)
            {
                flashLight.intensity -= scroll.y;
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
        StartCoroutine(flickerLight());
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
}
