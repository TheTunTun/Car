using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SteeringWheel : MonoBehaviour,IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private bool wheelIsheld = false;
    [SerializeField]
    private RectTransform wheel; // transform of the wheel gui
    [SerializeField]
    private CarControl1 control;

    [SerializeField]private camera cameraMode;
    [SerializeField] private Transform steeringModel;
    
    [SerializeField]
    private int forward = 0;

    private float wheelAngle;
    private float lastWheelAngle;

    private float maxSteerAngle = 200f;
    private float releaseSpeed = 300f; // the speed at which the steering wheel returns to it's original angle

    public float output;
    

    private Vector2 center;

    

    public void setForward(int f)
    {
        forward = f;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

   

    // Update is called once per frame
    void Update()
    {
        if(!wheelIsheld && wheelAngle != 0f)// let go the wheel and wheelangle > 0
        {
            float deltaAngle = releaseSpeed * Time.deltaTime; // the rate the wheel returns to its original position
            //Debug.Log("deltaAngle" + deltaAngle);
            if(Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
            {
                //Debug.Log(Mathf.Abs(deltaAngle) + "and" + Mathf.Abs(wheelAngle));
                wheelAngle = 0;
            }else if(wheelAngle > 0)
            {
                wheelAngle -= deltaAngle;
            }
            else
            {
                wheelAngle += deltaAngle;
            }

        }
        wheel.localEulerAngles = new Vector3(0, 0, -maxSteerAngle * output);//have to put - infront of maxsteerangle because the gui show up in reverse
        output = wheelAngle / maxSteerAngle;


        //control.Drive(forward, output);
        control.horizontal = output;
        control.vertical = forward;

        //Debug.Log(wheel.rotation);

        steeringModel.localEulerAngles = wheel.localEulerAngles;

    }

    

    public void OnPointerDown(PointerEventData data)//touch the steering wheel
    {
        wheelIsheld = true;
        //Debug.Log(wheelIsheld);
        
        center = RectTransformUtility.WorldToScreenPoint(data.pressEventCamera, wheel.position);//center of the wheel, convert the position of the wheel to its positon on the camera
        //Debug.Log( "wheel.position" + wheel.position);
        //Debug.Log("center" + center);
        lastWheelAngle = Vector2.Angle(Vector2.up, data.position - center);//the angle you touch the wheel
       
        //Debug.Log("lastwheelangle "+lastWheelAngle);

    }

    public void OnDrag(PointerEventData data)
    {
        //Debug.Log(data.ToString());
        float NewAngle = Vector2.Angle(Vector2.up, data.position - center);// the angle you have dragged to
        //Debug.Log("new angle" + NewAngle);
        if((data.position - center).sqrMagnitude >= 400)// if the distance between center and the dragged position is greater than 400
        {
            if (data.position.x > center.x)
            {
                wheelAngle += NewAngle - lastWheelAngle;
            }
            else
            {
                wheelAngle -= NewAngle - lastWheelAngle;
            }
            //Debug.Log("wheel angle" + wheelAngle);//how much angle you moved
            wheelAngle = Mathf.Clamp(wheelAngle, -maxSteerAngle, maxSteerAngle);
            lastWheelAngle = NewAngle;
        }
    }

    public void OnPointerUp(PointerEventData data)//let go the steering wheel
    {
        OnDrag(data);
        wheelIsheld = false;
        //Debug.Log(wheelIsheld);
        //Debug.Log(data.ToString());
    }
}
