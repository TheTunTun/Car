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

    [SerializeField] private AudioSource radio;

    [SerializeField] private AudioSource gear;
    
    [SerializeField] private CarControl1 control;
    private AudioListener audioListener;

    [SerializeField] private float minPitch = 0.4f;
    [SerializeField] private float maxPitch = 1.2f;

    [SerializeField] private float minImpact = 0.2f;
    [SerializeField] private float maxImpact = 1f;

    [SerializeField] private float maxSpeed = 120f;

    [SerializeField] private CarCustomization carCustomization;

    public bool gearChanged { get; set; }
    [SerializeField]private bool radioIsPlaying;
    public bool isBraking { get; set; }

    [SerializeField] private brakeDust dust;

    private void Awake()
    {
        //audioListener = GetComponent<AudioListener>();
        carCustomization.customize += EnginePaused;
        carCustomization.resumeGame += EngineResume;
        radioIsPlaying = false;
    }

    public void playRadio()
    {

        radio.Play();
        
    }
    public void stopRadio() { radio.Stop(); }
    void Enginesound()
    {
        
        float enginePitchRange = maxPitch - minPitch;
        float absoluteSpeed = Mathf.Abs(control.speedOnKm);
        float normalizedSpeed = absoluteSpeed / maxSpeed;

        engine.pitch = minPitch + enginePitchRange * normalizedSpeed;
        
    }

    public void EngineStart() { engine.Play(); }

    public void BrakeSound()
    {   
        
        if (brake.isPlaying == false && control.speedOnKm > 60)
        {
            brake.Play();
            //Debug.Log("brake sound");
            dust.StartDust();
            
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

    public void EnginePaused()
    {
        engine.Pause();
    }

    public void EngineResume() { engine.UnPause(); }

    public void Impact()
    {
        float impactSoundRange = maxImpact - minImpact;
        float normalizedSpeed = control.speedOnKm / maxSpeed;
        impact.volume = minImpact + impactSoundRange * normalizedSpeed;
        //Debug.Log(impact.volume);
        impact.Play();
    }

    public void FuelPickUP(AudioSource fuelsound)
    {
        fuelsound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Enginesound();
        
        
        if(gear.isPlaying == false) { gearChanged = false; }
        if(brake.isPlaying == false )
        {
            dust.StopDust();
        }
        
        
    }
}
