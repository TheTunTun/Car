using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Transform needle;
    [SerializeField]
    private Transform fuelLabelTemplateTransform;
    [SerializeField]
    private CarControl1 control;

    private const float MAX_FUEL_ANGLE = 30;//values are oposite because unity rotate counterclock wise
    private const float ZERO_FUEL_ANGLE = 150;

    

    void Start()
    {
        
    }

    private void Awake()
    {
        fuelLabelTemplateTransform.gameObject.SetActive(false);
        CreateFuelLabels();
    }

    // Update is called once per frame
    void Update()
    {
        
        needle.eulerAngles = new Vector3(0, 0, GetFuelRotation());
        
    }

    private float GetFuelRotation()
    {
        float totalAngleSize =  ZERO_FUEL_ANGLE - MAX_FUEL_ANGLE;
        float fuelNormalized = control.fuel / control.fuelMax; 
        return ZERO_FUEL_ANGLE - fuelNormalized * totalAngleSize;
    }

    private void CreateFuelLabels()
    {
        int labelAmount = 2;

        float totalAngleSize = ZERO_FUEL_ANGLE - MAX_FUEL_ANGLE;

        for (int i = 0; i <= labelAmount; i++)
        {
            Transform fuelLabelTransform = Instantiate(fuelLabelTemplateTransform, transform);
            float fuelLabelNormalized = (float)i / labelAmount;
            float fuelLabelAngle = ZERO_FUEL_ANGLE - fuelLabelNormalized * totalAngleSize;
            fuelLabelTransform.eulerAngles = new Vector3(0, 0, fuelLabelAngle);
            
            
            fuelLabelTransform.gameObject.SetActive(true);
        }

        needle.SetAsLastSibling();

    }

}
