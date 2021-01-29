using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerScript : MonoBehaviour
{
    // Start is called before the first frame update



    [SerializeField] private AudioSource impact;

    [SerializeField] private AudioSource gear;
    
    
    private AudioListener audioListener;

    public bool gearChanged { get; set; }
    

    public void GearChange()
    {
        if(gear.isPlaying == false)
        {
            gear.Play();
            gearChanged = true;
        }
        
    }

    

    

   

    

    public void FuelPickUP(AudioSource fuelsound)
    {
        fuelsound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (gear.isPlaying == false) { gearChanged = false; }
    }

    
}
