using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBodyCollide : MonoBehaviour
{

    private MeshCollider carBody;
    private bool collidable = false;
    [SerializeField] AudioManagerScript audioManager;

    // Start is called before the first frame update
    void Start()
    {
        carBody = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collidable == true)
        {
            float collisionForce = collision.impulse.magnitude / Time.fixedDeltaTime;
            audioManager.Impact(collisionForce);
            Debug.Log(collisionForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collidable = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        collidable = false;
    }
}
