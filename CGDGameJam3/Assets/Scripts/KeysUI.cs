using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{
    public Image[] keyImages;

    public bool key1Held;
    public bool key2Held;
    public bool key3Held;
    private void Awake()
    {
        keyImages = GetComponentsInChildren<Image>();


    }
    // Start is called before the first frame update
    void Start()
    {
        foreach (Image key in keyImages)
            key.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (key1Held)
            keyImages[0].enabled = true;
        if (key2Held)
            keyImages[1].enabled = true;
        if (key3Held)
            keyImages[2].enabled = true;
    }
}
