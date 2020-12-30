
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class brakeScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    bool pointerDown = false;
    // Start is called before the first frame update
    public SteeringWheel steer;
    [SerializeField]
    private CarControl1 control;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pointerDown)
        {
            
            control.Brake();
        }
        
    }
}
