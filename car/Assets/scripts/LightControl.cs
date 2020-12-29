using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{

    [Range(0, 2)]
    private int lightMode = 0;
    [SerializeField]
    protected Light[] headLights;
    [SerializeField]
    private float lowBeanRotation = 26;
    [SerializeField]
    private float lowBeanIntensity = 2;
    [SerializeField]
    private float lowBeanRange = 10;
    [SerializeField]
    private float lowBeanSpotAngle = 58;


    [SerializeField]
    private float highBeanRotation = 10;
    [SerializeField]
    private float highBeanIntensity = 4;
    [SerializeField]
    private float highBeanRange = 20;
    [SerializeField]
    private float highBeanSpotAngle = 100;

    [SerializeField]
    private GameObject backLight;
    [SerializeField]
    private Material backLightOff;
    [SerializeField]
    private Material backLightOn;

    // Start is called before the first frame update

    public void ChangeLight()
    {


        foreach (Light light in headLights)
        {
            var currentLocation = light.transform.localRotation;


            switch (lightMode)
            {
                case 0:
                    light.intensity = 0;
                    //bug.Log("off");

                    break;
                case 1:
                    light.intensity = lowBeanIntensity;

                    light.transform.localRotation = Quaternion.Euler(lowBeanRotation, currentLocation.y, 0);
                    light.range = lowBeanRange;
                    light.spotAngle = lowBeanSpotAngle;
                    Debug.Log("low");
                    break;
                case 2:
                    light.intensity = highBeanIntensity;

                    light.transform.localRotation = Quaternion.Euler(highBeanRotation, currentLocation.y, 0);
                    light.range = highBeanRange;
                    light.spotAngle = highBeanSpotAngle;
                    Debug.Log("high");
                    break;

            }


        }

        if (lightMode != 2)
        {
            lightMode++;
        }
        else
        {
            lightMode = 0;
        }
    }

    void Start()
    {
        
    }

    public void ChangeBacklight(bool lighton)
    {
        if (lighton == true)
        {
            backLight.GetComponent<MeshRenderer>().material = backLightOn;
        }
        else
        {
            backLight.GetComponent<MeshRenderer>().material = backLightOff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
