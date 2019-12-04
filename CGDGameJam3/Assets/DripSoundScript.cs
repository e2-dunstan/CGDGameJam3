using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DripSoundScript : MonoBehaviour
{
    public ParticleSystem dripPS;

    [FMODUnity.EventRef]
    public string dripSound = "";

    public int numberOfParticles = 0;
    public int lastNumOfParticles = 0;

    ParticleSystem.Particle[] m_Particles;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Initialise();
        numberOfParticles = dripPS.GetParticles(m_Particles);
        Debug.Log(m_Particles.Length);

        if(lastNumOfParticles != numberOfParticles && lastNumOfParticles < numberOfParticles)
        {
            lastNumOfParticles = numberOfParticles;
            FMODUnity.RuntimeManager.PlayOneShot(dripSound, this.transform.position);
        }
        else
        {
            lastNumOfParticles = numberOfParticles;
        }
     
    }

    void Initialise()
    {
        if(m_Particles == null || m_Particles.Length < dripPS.main.maxParticles)
        {
            m_Particles = new ParticleSystem.Particle[dripPS.main.maxParticles];
        }
    }
}
