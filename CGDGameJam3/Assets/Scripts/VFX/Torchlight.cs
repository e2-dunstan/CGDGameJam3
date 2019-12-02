using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torchlight : MonoBehaviour
{
    private bool initialised;
    [SerializeField] private Light lightSource;

    [SerializeField] private Color torchColour;
    [SerializeField] private Color lightColour;
    [SerializeField] bool bothHaveSameColour;

    [SerializeField] private bool disable;
    //Shutdown is for optimisation
    bool shutDown;
    private bool _active;
    
    [SerializeField] bool ignoreForceOn; //Lights will not turn on when the player is close
    [SerializeField] bool justPlay; //Play completely independently from the rest of the script

    // Start is called before the first frame update
    void Start()
    {
        initialised = false;
        _active = false;
        shutDown = false;
        torchColour.a = 1;

    }
    private void OnEnable()
    {
       

    }
    // Update is called once per frame
    void Update()
    {
        if (!justPlay)
        {
            if (lightSource.intensity == 0)
            {
                KillTorch();
                return;
            }

            if (!disable)
            {
                shutDown = DistanceManager.Instance().IsPlayerThisClose(15, transform.position);
                Initialise();
                DieIfFarFromPlayer();
                DieIfCloseToEnemy();
            }
            else
            {
                shutDown = true;
                KillTorch();
            }
        }
        else
        {
            _active = true;
            Initialise();
        }
    }

    private void DieIfCloseToEnemy()
    {
        for (int i = 0; i < DistanceManager.Instance().enemies.Count; i++)
        {
            if (DistanceManager.Instance().IsEnemyThisClose(7, i, transform.position)
                && !shutDown)
            {
                KillTorch();
            }
        }
    }

    private void DieIfFarFromPlayer()
    {
        if (shutDown)
        {
            KillTorch();
        }
        else
        {
            ReviveTorch();
        }
    }

    private void Initialise()
    {
        if (!initialised)
        {
            //Add an instance of the torch particle systems to the pool
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

                ParticleSystem.MainModule flameColour = VFXManager.Instance().GetAssignedParticleSystem(
                    gameObject, VFXManager.Instance().torchPSList).main;
                flameColour.startColor = torchColour;
                ParticleSystem.ColorOverLifetimeModule flameColourOL = VFXManager.Instance().GetAssignedParticleSystem(
                    gameObject, VFXManager.Instance().torchPSList).colorOverLifetime;
                flameColourOL.color = torchColour;

                if (bothHaveSameColour)
                    lightSource.color = torchColour;
                else
                    lightSource.color = lightColour;
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
        lightSource.enabled = false;
    }

    public void ReviveTorch()
    {
        if (!ignoreForceOn)
        {
            if (!DistanceManager.Instance().IsEnemyThisClose(7, 2, transform.position))
            {
                if (!_active)
                {
                    StopEffect(VFXManager.Instance().torchDeathPSList);
                }

                lightSource.enabled = true;
                _active = true;
            }
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
