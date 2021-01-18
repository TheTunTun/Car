using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform groundDetector;
    [SerializeField] private float rayLength = 1;
    [SerializeField] private CarControl1 control;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray groundRay = new Ray(groundDetector.position, Vector3.down);
        Debug.DrawRay(groundDetector.position, Vector3.down * rayLength, Color.red);
        if (Physics.Raycast(groundRay, out hit, rayLength))
        {
            if (hit.collider.tag == "Ground")
            {
                //Debug.Log("ground hit");
                control.isGrounded = true;
            }
        }
        else
        {
            control.isGrounded = false;
        }
    }
}
