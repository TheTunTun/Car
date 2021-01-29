using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBodyCollide : MonoBehaviour
{

    private MeshCollider carBody;
    private bool collidable = false;
    private AudioSource impact;

    [SerializeField] private float minImpactVolume = 0.2f;
    [SerializeField] private float maxImpactVolume = 1f;

    [SerializeField] private float maxImpact = 1000f;
    [SerializeField] private float maxSpeed = 120f;

    // Start is called before the first frame update
    void Start()
    {
        carBody = this.GetComponent<MeshCollider>();
        impact = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Impact(float i)
    {
        float impactSoundRange = maxImpactVolume - minImpactVolume;
        if (i > maxImpact) { i = maxImpact; }
        float normalizedImpact = i / maxImpact;

        impact.volume = minImpactVolume + impactSoundRange * normalizedImpact;
        //Debug.Log(impact.volume);
        impact.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if(collidable == true)
        {
            float collisionForce = collision.impulse.magnitude / Time.fixedDeltaTime;
            Impact(collisionForce);
            //Debug.Log(collisionForce);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        collidable = true;
    }
    private void OnTriggerExit(Collider other)
    {
        collidable = false;
    }
}
