using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    [SerializeField]
    private Transform needle;
    [SerializeField]
    private Transform speedLabelTemplateTransform;
    

    [SerializeField]
    private CarControl1 control;


    private const float MAX_SPEED_ANGLE = -20;//values are oposite because unity rotate counterclock wise
    private const float ZERO_SPEED_ANGLE = 210;

    

    private float speedMax;
    private float speed;

    // Start is called before the first frame update
    private void Awake()
    {
        speed = 0f;
        speedMax = 120f;
        speedLabelTemplateTransform.gameObject.SetActive(false);
        CreateSpeedLabels();//create labels
    }

    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Round(control.speedOnKm);
        Debug.Log("speed" + speed);
        if(speed > speedMax)
        {
            //speed = speedMax;
        }
        needle.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
    }

    private float GetSpeedRotation()
    {
        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        float speedNormalized = speed / speedMax;

        return ZERO_SPEED_ANGLE - speedNormalized * totalAngleSize;// if speed = 120, normalized = 1, 
                                                                    //so, 230 - 250 = -20 = MAX_SPEED_ANGLE
    }

    private void CreateSpeedLabels()
    {
        int labelAmount = 5;

        float totalAngleSize = ZERO_SPEED_ANGLE - MAX_SPEED_ANGLE;

        for(int i = 0; i <= labelAmount; i++)
        {
            Transform speedLabelTransform = Instantiate(speedLabelTemplateTransform, transform);
            float speedLabelNormalized = (float)i / labelAmount;
            float speedLabelAngle = ZERO_SPEED_ANGLE - speedLabelNormalized * totalAngleSize;
            speedLabelTransform.eulerAngles = new Vector3(0, 0, speedLabelAngle);
            speedLabelTransform.Find("speedText").GetComponent<Text>().text = Mathf.RoundToInt(speedLabelNormalized * speedMax).ToString();
            speedLabelTransform.Find("speedText").eulerAngles = Vector3.zero;
            speedLabelTransform.gameObject.SetActive(true);
        }

        needle.SetAsLastSibling();

    }
}
