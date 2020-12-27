using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private WheelCollider[] WC;
    [SerializeField]
    private GameObject[] wheels;
    [SerializeField]
    private float torque = 200;
    [SerializeField]
    private float braketorque = 30000;
    [SerializeField]
    private float maxSteerAngle = 30;
    [SerializeField]
    private Rigidbody carbody;
    private int lightMode = 0;
    [SerializeField]
    private Light[] headLights;
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
   


    void Start()
    {
        ChangeLight();
    }

    void ChangeLight()
    {
        

        foreach(Light light in headLights)
        {
            var currentLocation = light.transform.localRotation;

            switch (lightMode)
            {
                case 0:
                    light.intensity = 0;
                    Debug.Log("off");
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
    }

    void Drive(float acceleration, float steer)
    {
        acceleration = Mathf.Clamp(acceleration, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1);
        float thrustTorque = acceleration * torque;
        float carturn = steer * maxSteerAngle;
        //Debug.Log(thrustTorque);
        for(int i = 0; i < 4; i++)
        {
            WC[i].brakeTorque = 0;

            
            //Debug.Log("driving");
            if(i == 2 || i == 3)
            {
                if(WC[i].rpm < 500)
                {
                    WC[i].motorTorque = thrustTorque;
                }
                else
                {
                    WC[i].motorTorque = 0;
                }
            }

            if (i == 0 || i == 1) 
            {
                WC[i].steerAngle = carturn;
            }

            Quaternion wheelRotation;
            Vector3 wheelPosition;

            WC[i].GetWorldPose(out wheelPosition, out wheelRotation);// get the positionn of the wheel colliders
            wheels[i].transform.position = wheelPosition;//assign that collider positionn to the wheel mesh
            wheels[i].transform.rotation = wheelRotation;//give that collider rotation to the wheel meesh

        }
    }

    void Brake()
    {
        ChangeBacklight(true);
        for(int i = 0; i < 4; i++)
        {
            WC[i].brakeTorque = carbody.mass * braketorque;
            WC[i].motorTorque = 0;
           
        }
    }

    void ChangeBacklight(bool lighton)
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
        float a = Input.GetAxis("Vertical");
        float b = Input.GetAxis("Horizontal");
        
        
        Drive(a,b);
        if (Input.GetKey(KeyCode.Space))
        {
            Brake();

        }
        else
        {
            ChangeBacklight(false);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (lightMode != 2)
            {
                lightMode++;
            }
            else
            {
                lightMode = 0;
            }
            ChangeLight();
        }
    }
}
