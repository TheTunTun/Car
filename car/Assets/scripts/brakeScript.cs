
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class brakeScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    // Start is called before the first frame update
    
    [SerializeField]
    private CarControl1 control;

    public void OnPointerDown(PointerEventData eventData)
    {
        control.braking(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        control.braking(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
