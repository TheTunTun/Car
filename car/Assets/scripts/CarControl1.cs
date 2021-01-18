using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CarControl1 : MonoBehaviour

    
{
    // Start is called before the first frame update
    [SerializeField] private AudioManagerScript audioManager;
    [SerializeField] private WheelCollider[] WC;
    [SerializeField] private GameObject[] wheels;
    [SerializeField] private float torque = 200;
    [SerializeField] private float braketorque = 30000;
    [SerializeField] private float maxSteerAngle = 30;
    [SerializeField] private Rigidbody carbody;
    [SerializeField] private float wheelRadius = 0.4f;
    [SerializeField] private float fuelEfficiency = 0.01f;
    [SerializeField] private float reduceSpeedRate = 1;
    [SerializeField] private Text engineOn;
    public float rpmLimitReached { get; set; }


    
    private float rpm = 0;

    private float rpmLimit = 300;
    public float speedOnKm { get; set; }

    public float fuelMax { get; set; }
    public float fuel { get; set; }

    
    public float gear { get; set; }

    public float vertical { get; set; }
    public float horizontal { get; set; }

    public float forward { get; set; }
    public bool isGrounded { get; set; }
    public bool EngineIsOn { get; set; }
   
    

    public Action<bool> braking;

    
    private LightControl lightControl;

    


    private void Awake()
    {
        lightControl = GetComponent<LightControl>();
        lightControl.ChangeLight();
        fuelMax = 30;
        fuel = fuelMax;

        braking += Brake;
        rpmLimitReached = 0;
        EngineIsOn = false;
        engineOn.text = "Off";
        
    }
    void Start()
    {
        
    }

    public void StartEngine()
    {
        if(EngineIsOn == true) { EngineIsOn = false; engineOn.text = "Off"; audioManager.EngineStopped(); }
        else if(EngineIsOn == false){ EngineIsOn = true; engineOn.text = "On"; audioManager.EngineStart(); }
    }

    public void Drive(float acceleration, float steer)
    {
        if (acceleration == 0 && isGrounded && EngineIsOn) { IncreaseDrag(); }
        else if(isGrounded && !EngineIsOn) { IncreaseDrag(); }
        else { ReduceDrag(); }
        //Debug.Log("acce"+acceleration);
        acceleration = Mathf.Clamp(acceleration, -1, 1);
        //Debug.Log("acce" + acceleration + "steer" + steer);
        steer = Mathf.Clamp(steer, -1, 1);
        float thrustTorque = acceleration * torque * forward;
        float carturn = steer * maxSteerAngle;
        
        for(int i = 0; i < WC.Length; i++)
        {

            //Debug.Log("driving");
            if ((i == 2 || i == 3) && EngineIsOn)
            {   
      
                if(Mathf.Abs(WC[i].rpm) < rpmLimit)
                    {
                        
                        WC[i].motorTorque = thrustTorque;
                        
                }else{   WC[i].motorTorque = 0;  }

                rpm = WC[i].rpm;
                if(rpm >= rpmLimit - 1)
                {
                    rpmLimitReached = rpmLimitReached + 1 * Time.deltaTime;

                }
                else
                {
                    rpmLimitReached = 0;
                }

                //Debug.Log("rpmlimitReached "+rpmLimitReached);
            }

            if (i == 0 || i == 1) 
            {
                WC[i].steerAngle = carturn;
            }

            Quaternion wheelRotation;
            Vector3 wheelPosition;

            
            audioManager.isBraking = false;

            WC[i].GetWorldPose(out wheelPosition, out wheelRotation);// get the positionn of the wheel colliders
            wheels[i].transform.position = wheelPosition;//assign that collider positionn to the wheel mesh
            wheels[i].transform.rotation = wheelRotation;//give that collider rotation to the wheel meesh
            
        }


    }

   

     public void Brake(bool isBreaking)
    {
        
        if (isBreaking)
        {
            lightControl.ChangeBacklight(true);
            for (int i = 0; i < 4; i++)
            {
                WC[i].brakeTorque = carbody.mass * braketorque;

                WC[i].motorTorque = 0;
                //Debug.Log("braked");


            }
            audioManager.BrakeSound();
        }
        else
        {
            lightControl.ChangeBacklight(false);
            for (int i = 0; i < 4; i++)
            {
                WC[i].brakeTorque = 0;

            }
            
        }

        
    }

    // Update is called once per frame
    

    public void FuelSystem()
    {
        
        fuel -= Mathf.Abs(speedOnKm) * fuelEfficiency * Time.deltaTime;
        if(fuel < 0) { fuel = 0; }
        //Debug.Log("fuel" + fuel);
        if(fuel == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                WC[i].brakeTorque = carbody.mass/2;

                WC[i].motorTorque = 0;
                //Debug.Log("braked");
                audioManager.EngineStopped();
            }
        }
    }

    public void Speed()
    {
        float circumference = 2f * 3.14f * wheelRadius; //Circumference of a circle C = 2 pi r 
        float speedOnMeter = (circumference * rpm) * 60;  //metre per hour
        speedOnKm = speedOnMeter / 1000;
        //Debug.Log("Speed "+speedOnKm + " km" + "rpm" + rpm);
        
        
    }

    public void RpmLimiter(float g)
    {
        gear = g;
        switch (g)
        {
            case 0:
                rpmLimit = 300;
                forward = -1;
                break;

            case 1:
                rpmLimit = 300;
                forward = 1;
            break;
            case 2:
                rpmLimit = 400;
                forward = 1;
                break;
            case 3:
                rpmLimit = 500;
                forward = 1;
                break;
            case 4:
                rpmLimit = 600;
                forward = 1;
                break;
            case 5:
                rpmLimit = 700;
                forward = 1;
                break;

        }
        //Debug.Log("gear " + gear + "rpmlimit " + rpmLimit);

    }

    void IncreaseDrag()
    {
        GetComponent<Rigidbody>().drag = reduceSpeedRate;
    }

    void ReduceDrag()
    {
        GetComponent<Rigidbody>().drag = 0;
    }
    
    void Update()
    {
        
        Drive(vertical, horizontal);
        
        Speed();
        FuelSystem();
        if(gear == 0) { lightControl.ChangeReverseLight(true); } else { lightControl.ChangeReverseLight(false); }

    }


}
