using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform objectfollow; //the car
    public Vector3 offset;
    public float followSpeed = 10;
    public float lookSpeed = 10;

    public void LookAtTarget()
    {
        Vector3 lookDirection = objectfollow.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime); //current rotation position to the car rotation.


    }

    public void MoveToTarget()
    {
        Vector3 targetPos = objectfollow.position +
                            objectfollow.forward * offset.z +
                            objectfollow.right * offset.x +
                            objectfollow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);// go between one value to another
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }
}
