using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fuelbarrel : MonoBehaviour
{

    [SerializeField] private CarControl1 control;
    [SerializeField] private MeshCollider car;
    [SerializeField] private float fuelAmount = 10;
    [SerializeField] private AudioManagerScript audioManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 100 * Time.deltaTime, 0, Space.World);
    }

    public void addFuel()
    {
        control.fuel += fuelAmount;
        if(control.fuel > control.fuelMax)
        {
            control.fuel = control.fuelMax;
        }

        audioManager.FuelPickUP();
        Destroy(this.gameObject, 0.4f);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other == car)
        {
            addFuel();
        }
        
    }

}
