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

    void Start()
    {
        
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

            if(acceleration != 0)
            {
                WC[i].brakeTorque = 0;
                WC[i].motorTorque = thrustTorque;//rotate the wheel collider
                Debug.Log("driving");
                

            }
            else if(acceleration == 0)
            {
                WC[i].brakeTorque = braketorque;//braking
                Debug.Log("braking");

            }

            if (i < 3)
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

    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis("Vertical");
        float b = Input.GetAxis("Horizontal");
        Drive(a,b);
    }
}
