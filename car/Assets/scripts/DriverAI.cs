using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DriverAI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject target;
    [SerializeField] private CarControl1 control;
    [SerializeField] private CarSound carSound;
    [SerializeField] private float brakeDistance = 2;
    [SerializeField] private float driveDistance = 3;
    [SerializeField] private float maxTargetDistance = 6;
   
    
    void Start()
    {
        control = this.GetComponent<CarControl1>();
        control.StartEngine();
        
        control.RpmLimiter(2);
        this.transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    
        
    private void LateUpdate()
    {
        
        Accelerate();
        TargetCheck();

    }

    void TargetCheck()
    {
        float distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
        if (distanceToTarget > maxTargetDistance)
        {
            target.GetComponent<CarPathFinder>().Wait();
        }
        else { target.GetComponent<CarPathFinder>().Continue(); }
    }


    void Accelerate()
    {

        Vector3 targetDirectional = this.transform.InverseTransformPoint(target.transform.position);
        if (targetDirectional.x > 1 || targetDirectional.x < -1) { control.horizontal = targetDirectional.x; }
        else { control.horizontal = 0; }

        float distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);

        if (distanceToTarget > driveDistance)
        {
            control.vertical = 1;
        }
        else
        {
            control.vertical = 0;
        }

        if (distanceToTarget < brakeDistance)
        {
            control.Brake(true);

        }
        else { control.Brake(false); }
        
        
    }
}
