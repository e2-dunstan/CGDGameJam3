using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private KeyManager keyManager;

    public void SetManagerReference(KeyManager _manager)
    {
        keyManager = _manager;
    }

    public void KeyActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyManager.KeyCollected(this);
        }
    }
}
