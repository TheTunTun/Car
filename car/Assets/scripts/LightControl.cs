using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightControl : MonoBehaviour
{

    [Range(0, 2)]private int lightMode = 2;
    [SerializeField]private Light[] fogLights;
    [SerializeField] private GameObject fogLightObject;

    [SerializeField]private Light[] mainLights;
    [SerializeField] private GameObject mainLightObject;

    [SerializeField] private GameObject rearLight;
    [SerializeField] private GameObject dayTimeLight;

    [SerializeField] private Material lightOff;
    [SerializeField] private Material lightOn;
    

    float nextInterval;

    
    

    [SerializeField]private GameObject brakeLight;
    

    [SerializeField] private GameObject reverseLight;

   

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
                if(signal.GetComponent<MeshRenderer>().sharedMaterial == lightOff)
                {

                    signal.GetComponent<MeshRenderer>().material = lightOn;
                }
                else
                {
                    signal.GetComponent<MeshRenderer>().material = lightOff;
                }
                
                nextInterval = Time.time + 1;
            }

        }
        else
        {
            signal.GetComponent<MeshRenderer>().material = lightOff;
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
                fogLightObject.GetComponent<MeshRenderer>().material = lightOff;
                mainLightObject.GetComponent<MeshRenderer>().material = lightOff;
                rearLight.GetComponent<MeshRenderer>().material = lightOff;
                dayTimeLight.GetComponent<MeshRenderer>().material = lightOn;

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
                fogLightObject.GetComponent<MeshRenderer>().material = lightOn;
                mainLightObject.GetComponent<MeshRenderer>().material = lightOff;
                rearLight.GetComponent<MeshRenderer>().material = lightOn;
                dayTimeLight.GetComponent<MeshRenderer>().material = lightOff;

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
                fogLightObject.GetComponent<MeshRenderer>().material = lightOff;
                mainLightObject.GetComponent<MeshRenderer>().material = lightOn;
                rearLight.GetComponent<MeshRenderer>().material = lightOn;
                dayTimeLight.GetComponent<MeshRenderer>().material = lightOff;

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
            brakeLight.GetComponent<MeshRenderer>().material = lightOn;
        }
        else
        {
            brakeLight.GetComponent<MeshRenderer>().material = lightOff;
        }
    }

    public void ChangeReverseLight(bool lighton)
    {
        if (lighton == true)
        {
            reverseLight.GetComponent<MeshRenderer>().material = lightOn;
        }
        else
        {
            reverseLight.GetComponent<MeshRenderer>().material = lightOff;
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
