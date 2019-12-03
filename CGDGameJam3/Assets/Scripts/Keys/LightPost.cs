using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPost : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Light pointLight;
    [SerializeField] private Torchlight torchlight;

    void Start()
    {
    }

    public void SetLightActive(bool isActive)
    {
        Debug.Log("TURNING ON A LIGHT" + isActive);
        pointLight.gameObject.SetActive(isActive);
        torchlight.ManuallyStart();
    }
}
