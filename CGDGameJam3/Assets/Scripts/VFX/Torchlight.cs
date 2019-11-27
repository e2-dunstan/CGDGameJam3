using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchlight : MonoBehaviour
{
    [SerializeField] private bool initialised;
    public bool _active;
    //Shutdown is for optimisation
    bool shutDown;

    // Start is called before the first frame update
    void Start()
    {
        initialised = false;
        _active = true;
        shutDown = false;

    }
    private void OnEnable()
    {
       

    }
    // Update is called once per frame
    void Update()
    {
        shutDown = DistanceManager.Instance().PlayerDistance(15, transform.position);
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
                PlayEffect(VFXManager.Instance().torchPSList);
            }
        }

        if (shutDown)
        {
            KillTorch();
        }
        else
        {
            ReviveTorch();
        }
        for (int i = 0; i < DistanceManager.Instance().enemies.Count; i++)
        {
            if (DistanceManager.Instance().EnemyDistance(7, i, transform.position)
                && !shutDown)
            {
                KillTorch();
            }
        }
    }


    public void KillTorch()
    {
        if (_active)
        {
            StopEffect(VFXManager.Instance().torchPSList);
            StopEffect(VFXManager.Instance().torchDeathPSList);

            if (!shutDown)
            {
                PlayEffect(VFXManager.Instance().torchDeathPSList, new Vector3(
                    0, -0.25f, 0));
            }
        }
        _active = false;
    }

    public void ReviveTorch()
    {
        if (!DistanceManager.Instance().EnemyDistance(7, 2, transform.position))
        {
            if (!_active)
            {
                StopEffect(VFXManager.Instance().torchDeathPSList);
            }
            _active = true;
        }
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
