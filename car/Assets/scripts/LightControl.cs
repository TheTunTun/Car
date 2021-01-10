using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightControl : MonoBehaviour
{

    [Range(0, 2)]private int lightMode = 2;
    [SerializeField]private Light[] fogLights;
    [SerializeField]private Light[] mainLights;

    [SerializeField] private Material turnLightOff;
    [SerializeField] private Material turnLightOn;
    

    float nextInterval;

    
    

    [SerializeField]private GameObject brakeLight;
    [SerializeField]private Material brakeLightOff;
    [SerializeField]private Material brakeLightOn;

    [SerializeField] private GameObject reverseLight;
    [SerializeField] private Material reverseLightOff;
    [SerializeField] private Material reverseLightOn;

   

    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    public void TurnSignalOn(bool turn, GameObject signal)
    {
        if(turn == true)
        {
            Debug.Log("ON");
            Debug.Log(Time.time + " and " + nextInterval);
            if (Time.time > nextInterval)
            {
                if(signal.GetComponent<MeshRenderer>().sharedMaterial == turnLightOff)
                {

                    signal.GetComponent<MeshRenderer>().material = turnLightOn;
                }
                else
                {
                    signal.GetComponent<MeshRenderer>().material = turnLightOff;
                }
                
                nextInterval = Time.time + 1;
            }

        }
        else
        {
            signal.GetComponent<MeshRenderer>().material = turnLightOff;
        }
    }

   

    public void ChangeLight()
    {

        

        if (lightMode != 2)
        {
            lightMode++;
        }
        else
        {
            lightMode = 0;
        }

        switch (lightMode)
        {
            case 0:
                
                foreach (Light light in fogLights)
                {
                    light.enabled = false;
                }
                foreach (Light light in mainLights)
                {
                    light.enabled = false;
                }
                break;
            case 1:
                foreach (Light light in fogLights)
                {
                    light.enabled = true;
                }
                foreach (Light light in mainLights)
                {
                    light.enabled = false;
                }
                break;
            case 2:
                foreach (Light light in fogLights)
                {
                    light.enabled = false;
                }
                foreach (Light light in mainLights)
                {
                    light.enabled = true;
                }
                break;
        }

    }

   

    void Start()
    {
        
    }

    public void ChangeBacklight(bool lighton)
    {
        if (lighton == true)
        {
            brakeLight.GetComponent<MeshRenderer>().material = brakeLightOn;
        }
        else
        {
            brakeLight.GetComponent<MeshRenderer>().material = brakeLightOff;
        }
    }

    public void ChangeReverseLight(bool lighton)
    {
        if (lighton == true)
        {
            reverseLight.GetComponent<MeshRenderer>().material = reverseLightOn;
        }
        else
        {
            reverseLight.GetComponent<MeshRenderer>().material = reverseLightOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
