using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private CharacterController _controller;
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 30f;
    private float _gravity = -9.81f;
    float verticalMovement = 0;
    float horizontalMovement = 0;




    void Start()
    {
        _controller = GetComponent <CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        verticalMovement = Input.GetAxis("Vertical");
        if(verticalMovement != 0)
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            
        }
        else {

            horizontalMovement = 0;
            
        }

        Vector3 localDirection = new Vector3(verticalMovement, 0, 0); //Direction in local. There, x axis is vertical.
        Vector3 localVelocity = localDirection * _speed;


        transform.Rotate(0, horizontalMovement, 0); //rotate in the local y axix

        Vector3 worldVelocity = transform.TransformDirection(localVelocity); //change local velocity to world velocity;
        worldVelocity.y = _gravity;
        _controller.Move(worldVelocity * Time.deltaTime);
    }
}
