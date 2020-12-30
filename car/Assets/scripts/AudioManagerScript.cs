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
    
    [SerializeField] private AudioSource gear;
    
    [SerializeField] private CarControl1 control;
    private AudioListener audioListener;

    [SerializeField] private float minPitch = 0.4f;
    [SerializeField] private float maxPitch = 1.2f;
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
        float normalizedSpeed = control.speedOnKm / maxSpeed;

        engine.pitch = minPitch + enginePitchRange * normalizedSpeed;
        
    }

    public void BrakeSound()
    {
        if (brake.isPlaying == false && control.speedOnKm > 70)
        {
            brake.Play();
            Debug.Log("brake sound");
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

    // Update is called once per frame
    void Update()
    {
        Enginesound();
        
        if(gear.isPlaying == false) { gearChanged = false; }
        
    }
}
