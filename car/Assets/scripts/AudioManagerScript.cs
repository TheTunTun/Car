using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioSource engine;
    [SerializeField] private AudioSource brake;
    [SerializeField] private AudioSource honk;
    [SerializeField] private AudioSource impact;
    [SerializeField] private AudioSource fuelSound;

    
    [SerializeField] private AudioSource gear;
    
    [SerializeField] private CarControl1 control;
    private AudioListener audioListener;

    [SerializeField] private float minPitch = 0.4f;
    [SerializeField] private float maxPitch = 1.2f;

    [SerializeField] private float minImpact = 0.2f;
    [SerializeField] private float maxImpact = 1f;

    [SerializeField] private float maxSpeed = 120f;

    public bool gearChanged { get; set; }

    public bool isBraking { get; set; }

    private void Awake()
    {
        //audioListener = GetComponent<AudioListener>();
    }

    void Enginesound()
    {
        float enginePitchRange = maxPitch - minPitch;
        float absoluteSpeed = Mathf.Abs(control.speedOnKm);
        float normalizedSpeed = absoluteSpeed / maxSpeed;

        engine.pitch = minPitch + enginePitchRange * normalizedSpeed;
        
    }

    public void BrakeSound()
    {
        if (brake.isPlaying == false && control.speedOnKm > 60)
        {
            brake.Play();
            //Debug.Log("brake sound");
        }
        
    }

    public void GearChange()
    {
        if(gear.isPlaying == false)
        {
            gear.Play();
            gearChanged = true;
        }
        
    }

    public void Honking()
    {
        if(honk.isPlaying == false)
        {
            honk.Play();
            Debug.Log("honk");
        }
    }

    public void EngineStopped()
    {
        engine.Stop();
    }

    public void Impact()
    {
        float impactSoundRange = maxImpact - minImpact;
        float normalizedSpeed = control.speedOnKm / maxSpeed;
        impact.volume = minImpact + impactSoundRange * normalizedSpeed;
        //Debug.Log(impact.volume);
        impact.Play();
    }

    public void FuelPickUP()
    {
        fuelSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Enginesound();
        
        if(gear.isPlaying == false) { gearChanged = false; }
        
    }
}
