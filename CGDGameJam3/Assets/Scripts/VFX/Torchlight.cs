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
        //For testing
        if (InputHandler.Instance().GetSprintHold())
        {
            KillTorch();
        }
        if (InputHandler.Instance().GetSprintUp())
        {
            ReviveTorch();
        }

        //Add an instance of the torch particle systems to the pool
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
                PlayEffect(VFXManager.Instance().torchPSList, new Vector3(
                    0, 0.25f, 0));
            }
        }
    }
    public void KillTorch()
    {
        if (_active)
        {
            StopEffect(VFXManager.Instance().torchDeathPSList);
            StopEffect(VFXManager.Instance().torchPSList);

            PlayEffect(VFXManager.Instance().torchDeathPSList, new Vector3(
                0, 0.25f, 0));
        }
        _active = false;
    }

    public void ReviveTorch()
    {
        if (!_active)
        {
            StopEffect(VFXManager.Instance().torchDeathPSList);

            PlayEffect(VFXManager.Instance().torchDeathPSList, new Vector3(
                0, 0.25f, 0));
        }
        _active = true;
    }

    public void StopEffect(List<VFXManager.PartSys> _particleSystemList)
    {
        VFXManager.Instance().StopParticleSystemOnGameObject(gameObject, _particleSystemList);
    }

    public void PlayEffect(List<VFXManager.PartSys> _particleSystemList)
    {
        VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, _particleSystemList);
    }
    public void PlayEffect(List<VFXManager.PartSys> _particleSystemList, Vector3 offset)
    {
        VFXManager.Instance().PlayParticleSystemOnGameObject(gameObject, _particleSystemList, offset);
    }
}
