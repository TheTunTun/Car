
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;



public class brakeScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    // Start is called before the first frame update
    [SerializeField] private UnityEvent onBrakePressed;
    [SerializeField] private UnityEvent onBrakeReleased;
    
    [SerializeField]
    private CarControl1 control;

    public void OnPointerDown(PointerEventData eventData)
    {
        onBrakePressed.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        onBrakeReleased.Invoke();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
