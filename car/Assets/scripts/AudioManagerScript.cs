using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioSource engine;
    [SerializeField] private AudioClip engineStart;
    [SerializeField] private AudioClip engineNoice;
    [SerializeField] private AudioSource brake;

    [SerializeField] private AudioSource impact;

    [SerializeField] private AudioSource gear;
    
    [SerializeField] private CarControl1 control;
    private AudioListener audioListener;

    [SerializeField] private float minPitch = 0.4f;
    [SerializeField] private float maxPitch = 1.2f;

    [SerializeField] private float minImpactVolume = 0.2f;
    [SerializeField] private float maxImpactVolume = 1f;

    [SerializeField] private float maxImpact = 1000f;
    [SerializeField] private float maxSpeed = 120f;

    [SerializeField] private CarCustomization carCustomization;

    public bool gearChanged { get; set; }

    public bool isBraking { get; set; }

    [SerializeField] private brakeDust dust;

    private void Awake()
    {
        //audioListener = GetComponent<AudioListener>();
        carCustomization.customize += EnginePaused;
        carCustomization.resumeGame += EngineResume;

       



        control.noMoreFuel += EngineStopped;

    }

    private void OnDestroy()
    {
        control.noMoreFuel -= EngineStopped;
    }


    void Enginesound()
    {
           
        if(engine.clip == engineNoice)
        {
            
            float enginePitchRange = maxPitch - minPitch;
            float absoluteSpeed = Mathf.Abs(control.speedOnKm);
            float normalizedSpeed = absoluteSpeed / maxSpeed;

            engine.pitch = minPitch + enginePitchRange * normalizedSpeed;
        }
        
    }

    public void EngineStart() {
        engine.PlayOneShot(engineStart, .10f);
        engine.PlayDelayed(engineStart.length / 2);
        
        
    }

    IEnumerator WaitForEngine(float t)
    {
        
        yield return new WaitForSeconds(t);
        

    }

    public void EngineStopped()
    {
        
        engine.Stop();
    }

    public void EnginePaused()
    {
        engine.Pause();
    }

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

    

    

    public void EngineResume() { engine.UnPause(); }

    public void Impact(float i)
    {
        float impactSoundRange = maxImpactVolume - minImpactVolume;
        if(i > maxImpact) { i = maxImpact; }
        float normalizedImpact = i / maxImpact;
        
        impact.volume = minImpactVolume + impactSoundRange * normalizedImpact ;
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
