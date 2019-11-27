using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [Header("Object References")]
    public KeyPickup[] keys;
    public LightPost[] lightPosts;
    public KeysUI keysUI;

    private int keysUncollected;

    void Start()
    {
        foreach (var key in keys)
        {
            key.SetManagerReference(this);
        }

        foreach (var light in lightPosts)
        {
            light.SetLightActive(false);
        }

        keysUncollected = keys.Length;
    }

    public void KeyCollected(KeyPickup _key)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            if(_key == keys[i])
            {
                keysUI.ShowKeyUI(_key);
                _key.KeyActive(false);
                lightPosts[i].SetLightActive(true);
                keysUncollected--;

                if(keysUncollected == 0)
                {
                    AllKeysCollected();
                }
            }
        }
    }

    private void AllKeysCollected()
    {
        Debug.Log("All keys collected. Do something here to finish the game");
    }
}
