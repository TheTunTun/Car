using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody _rigidbody;
    float verticalMovement = 0;
    float horizontalMovement = 0;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalMovement = Input.GetAxis("Vertical");
        if (verticalMovement != 0)
        {
            horizontalMovement = Input.GetAxis("Horizontal");

        }
        else
        {

            horizontalMovement = 0;

        }


    }
}
