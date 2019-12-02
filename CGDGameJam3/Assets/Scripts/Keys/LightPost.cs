using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPost : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Light pointLight;

    void Start()
    {
    }

    public void SetLightActive(bool isActive)
    {
        pointLight.gameObject.SetActive(isActive);
    }
}
