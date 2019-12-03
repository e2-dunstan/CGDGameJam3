using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DripSoundScript : MonoBehaviour
{
    private ParticleSystem dripPS;

    [FMODUnity.EventRef]
    public string dripSound = "";

    public int numberOfParticles = 0;
    private ParticleSystem.Particle[] m_Particles;

    // Start is called before the first frame update
    void Start()
    {
        dripPS = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        dripPS.GetParticles(m_Particles);

        if(numberOfParticles != m_Particles.Length && numberOfParticles < m_Particles.Length)
        {
            numberOfParticles = m_Particles.Length;
            FMODUnity.RuntimeManager.PlayOneShot(dripSound, this.transform.position);
        }
        else
        {
            numberOfParticles = m_Particles.Length;
        }
    }
}
