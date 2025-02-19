﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCutscene : MonoBehaviour
{
    KeyManager keyManager;

    Camera[] camera;
    GameObject player;
    GameObject gate;
    List<Transform> gates = new List<Transform>();
    public Image uiImage;
    Image image;
    float speed = 2.0f;
    float finalFadeOutSpeed = 1.0f;
    bool fadedOut = false;
    bool fadedIn = false;
    bool doorOpen = false;
    bool finalFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {

            gate = GameObject.FindGameObjectWithTag("Gate");
            var g = gate.GetComponentsInChildren<Transform>();
            for (int i = 0; i < g.Length; i++)
            {
                gates.Add(g[i]);
            }

            gates.RemoveAt(4);
            gates.RemoveAt(2);
            gates.RemoveAt(0);

            camera = FindObjectsOfType<Camera>();
            player = GameObject.FindGameObjectWithTag("Player");
            image = player.GetComponentInChildren<Image>();
            keyManager = FindObjectOfType<KeyManager>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(fadedOut)
        {
            gate.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(FadeIn());
        }
        else if(fadedIn)
        {
            fadedIn = false;
            StartCoroutine(RotateDoors());
        }
        else if(doorOpen)
        {
            player.GetComponent<PlayerMovement>().walkForward = true;
            StartCoroutine(Walk());
        }
        
    }

    IEnumerator Walk()
    {
        float time = 0.0f;
        while (time < 2.0f)
        {
            time += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadeOut());
        finalFadeOut = true;
        yield return 0;
    }

    IEnumerator FadeIn()
    {
        while (image.color.a > 0.0f)
        {
            fadedOut = false;
            Fade(ref image, -Time.deltaTime * speed);
            yield return null;
        }

        fadedIn = true;
        yield return 0;
    }

    IEnumerator FadeOutUI()
    {
        while (image.color.a < 1.0f)
        {
            Fade(ref image, Time.deltaTime * speed);
            yield return null;
        }

        fadedOut = true;
        camera[1].enabled = false;
        camera[0].enabled = true;
        yield return 0;
    }

    IEnumerator FadeOut()
    {
        while (image.color.a < 1.0f)
        {
            Fade(ref image, Time.deltaTime * finalFadeOutSpeed);
            yield return null;
        }

        fadedOut = true;
        if (finalFadeOut)
        {
            SceneManager.LoadScene(2);
        }
        yield return 0;
    }

    IEnumerator RotateDoors()
    {
        Vector3 startRot = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 endRot = new Vector3(0.0f, 80.0f, 0.0f);
        float time = 0.0f;
        while (time < 1.0f)
        {
            time += Time.deltaTime;
            gates[0].eulerAngles = Vector3.Lerp(startRot, endRot, time);
            gates[1].eulerAngles = Vector3.Lerp(startRot, -endRot, time);
            yield return null;
        }
        doorOpen = true;
        yield return 0;
    }

    void Fade(ref Image _image, float a)
    {
        var colour = _image.color;
        colour = new Color(colour.r, colour.b, colour.b, colour.a + a);
        _image.color = colour;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && keyManager.AllKeysCollected())
        {
            player.GetComponent<PlayerMovement>().disableInput = true;
            player.GetComponent<PlayerMovement>().StopRunning();
            StartCoroutine(FadeOutUI());
        }
    }

}
