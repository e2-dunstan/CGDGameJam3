using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchlight : MonoBehaviour
{
    [SerializeField] private bool initialised;
    public bool _active;
    // Start is called before the first frame update
    void Start()
    {
        initialised = false;
        _active = true;
    }
    private void OnEnable()
    {
       

    }
    // Update is called once per frame
    void Update()
    {
        if (InputHandler.Instance().GetSprintHold())
        {
            KillTorch();
        }

        if (!initialised)
        {
            VFXManager.Instance().CreateParticleSystemForObject(
                VFXManager.Instance().torchPS,
                VFXManager.Instance().torchPSList);

            VFXManager.Instance().CreateParticleSystemForObject(
                VFXManager.Instance().torchDeathPS,
                VFXManager.Instance().torchDeathPSList);

            initialised = true;
        }
        else
        {
            if (_active)
            {
                VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, VFXManager.Instance().torchPSList, new Vector3(
                    0, 0.25f, 0));
            }

        }
    }
    public void KillTorch()
    {
        if (_active)
        {
            VFXManager.Instance().StopParticleSystemOnGameObject(gameObject, VFXManager.Instance().torchPSList);
            VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, VFXManager.Instance().torchDeathPSList, new Vector3(
                0, 0.25f, 0));
        }
        _active = false;
    }
}
