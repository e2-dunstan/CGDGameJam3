using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{
    public Image[] keyImages;


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
        
    }
}
