using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class CarControl1 : MonoBehaviour

    
{
    // Start is called before the first frame update
    private CarSound carSound;

    [SerializeField] private WheelCollider[] WC;
    [SerializeField] private GameObject[] wheels;
    [SerializeField] private float torque = 200;
    [SerializeField] private float braketorque = 30000;
    [SerializeField] private float maxSteerAngle = 30;
    [SerializeField] private Rigidbody carbody;
    [SerializeField] private float wheelRadius = 0.4f;
    [SerializeField] private float fuelEfficiency = 0.01f;
    [SerializeField] private float reduceSpeedRate = 1;
    [SerializeField] private Text engineText;
    public float rpmLimitReached { get; set; }


    
    public float rpm { get; set; }

    private float rpmLimit = 300;
    public float speedOnKm { get; set; }

    public float fuelMax { get; set; }
    public float fuel { get; set; }

    
    public float gear { get; set; }

    public float vertical { get; set; }
    public float horizontal { get; set; }

    public float forward { get; set; }
    public bool isGrounded { get; set; }

    public float formerRpmLimit = 300;
    
    private LightControl lightControl;

    private enum carState { engineOff, engineOn, noFuel}
    private carState currentCarState = carState.engineOff;

    [SerializeField] private bool isPlayerCar;

    public Action noMoreFuel;

    private void Awake()
    {
        lightControl = GetComponent<LightControl>();
        carSound = this.GetComponent<CarSound>();

        lightControl.ChangeLight();
        fuelMax = 30;
        fuel = fuelMax;
        rpmLimitReached = 0;
        
        engineText.text = "Off";
        rpm = 0;
        noMoreFuel += NoMoreFuel;
    }

    private void OnDestroy()
    {
        noMoreFuel -= NoMoreFuel;
    }


    void Start()
    {
        
    }

    public void StartEngine()
    {
        if(currentCarState != carState.noFuel)
        {
            currentCarState = carState.engineOn;
            engineText.text = "On";
        }
    }

    public void NoMoreFuel()
    {
        currentCarState = carState.noFuel;
        engineText.text = "Empty";
    }

    public void StopEngine()
    {
        if (currentCarState != carState.noFuel)
        {
            currentCarState = carState.engineOff;
            engineText.text = "Off";
        }
    }

    public void Drive(float acceleration, float steer)
    {
        //Debug.Log("driving"+ acceleration);   
        if (acceleration == 0 && isGrounded ) { IncreaseDrag(); }
        else { ReduceDrag(); }
        
        acceleration = Mathf.Clamp(acceleration, -1, 1);
        //Debug.Log("acce" + acceleration + "steer" + steer);
        steer = Mathf.Clamp(steer, -1, 1);

        float thrustTorque = acceleration * torque * forward;
        float carturn = steer * maxSteerAngle;
        
        for(int i = 0; i < WC.Length; i++)
        {
            
            
            if (i == 2 || i == 3)
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

                //Debug.Log("motortorque" + WC[i].brakeTorque);
                
            }

            if (i == 0 || i == 1) 
            {
                WC[i].steerAngle = carturn;
            }

            Quaternion wheelRotation;
            Vector3 wheelPosition;

            
            carSound.isBraking = false;

            WC[i].GetWorldPose(out wheelPosition, out wheelRotation);// get the positionn of the wheel colliders
            wheels[i].transform.position = wheelPosition;//assign that collider positionn to the wheel mesh
            wheels[i].transform.rotation = wheelRotation;//give that collider rotation to the wheel meesh
            
        }


    }

   

     public void Brake(bool isBreaking)
    {
        
        if (isBreaking)
        {
           
            for (int i = 0; i < 4; i++)
            {
                WC[i].brakeTorque = carbody.mass * braketorque;

                WC[i].motorTorque = 0;



            }
            carSound.BrakeSound();
        }
        else
        {
            
            for (int i = 0; i < 4; i++)
            {
                WC[i].brakeTorque = 0;

            }
            
        }

        
    }

    // Update is called once per frame
    

    public void FuelSystem()
    {

        if (isPlayerCar)
        {
            fuel -= Mathf.Abs(speedOnKm) * fuelEfficiency * Time.deltaTime;
            if (fuel < 0) { fuel = 0; }

            if (fuel == 0)
            {
                noMoreFuel.Invoke();
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
                formerRpmLimit = 0;
                rpmLimit = 300;
                forward = -1;
                break;

            case 1:
                formerRpmLimit = 0;
                rpmLimit = 300;
                forward = 1;
            break;
            case 2:
                formerRpmLimit = 300;
                rpmLimit = 400;
                forward = 1;
                break;
            case 3:
                formerRpmLimit = 400;
                rpmLimit = 500;
                forward = 1;
                break;
            case 4:
                formerRpmLimit = 500;
                rpmLimit = 600;
                forward = 1;
                break;
            case 5:
                formerRpmLimit = 600;
                rpmLimit = 700;
                forward = 1;
                break;

        }
        //Debug.Log("formerRpmLimit "+ formerRpmLimit);
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
        
        switch (currentCarState)
        {
            case carState.engineOn:
                
                Drive(vertical, horizontal);
                break;
            case carState.engineOff:
                Drive(0, horizontal);
                break;
            case carState.noFuel:
                Drive(0, horizontal);
                break;
        }

        Speed();
        FuelSystem();
        if(gear == 0) { lightControl.ChangeReverseLight(true); } else { lightControl.ChangeReverseLight(false); }

    }


}
