using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brakeDust : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] dustParticle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartDust()
    {
        foreach(ParticleSystem dust in dustParticle)
        {
            dust.Play();
        }
    }

    public void StopDust()
    {
        foreach (ParticleSystem dust in dustParticle)
        {
            dust.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
