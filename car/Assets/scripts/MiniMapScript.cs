using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapScript : MonoBehaviour
{
    [SerializeField] private Transform playerCar;
    [SerializeField] private Transform arrow;
    [SerializeField] private CarControl1 control;
    private float maxSpeed = 120f;
    private float minSpeedForZoom = 60f;
    private Camera minimapCamera;
    [SerializeField] private float minZoom = 8;
    [SerializeField] private float maxZoom = 15;

    

    public Action Zoom;
    public Action UnZoom;
    
    
    // Start is called before the first frame update
    void Start()
    {
        minimapCamera = GetComponent<Camera>();
        

      
    }

    
    

    // Update is called once per frame
    private void LateUpdate()
    {
        Vector3 cameraNewPosition = playerCar.position;//camera follows car
        cameraNewPosition.y = transform.position.y;
        transform.position = cameraNewPosition;
        transform.rotation = Quaternion.Euler(90f, playerCar.eulerAngles.y, 0f);
        

        if(control.speedOnKm >= minSpeedForZoom)//start zooming out minimap at a certain speed
        {
            float currentSpeed = control.speedOnKm;
            float zoomAmount = maxZoom - minZoom;
            float normalizedSPeed = currentSpeed / maxSpeed;
            float orthoSize = minZoom + zoomAmount * normalizedSPeed;
            if(orthoSize > maxZoom) { orthoSize = maxZoom; }
            minimapCamera.orthographicSize = orthoSize;
            Zoom.Invoke();
        }
        else
        {
            minimapCamera.orthographicSize = minZoom;
            UnZoom.Invoke();
        }

        
    }

    
}
