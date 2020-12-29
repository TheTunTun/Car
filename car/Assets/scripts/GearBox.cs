using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GearBox : MonoBehaviour
{
    private Slider gearBox;
    [SerializeField]
    private CarControl1 control;
    [SerializeField]
    private Text gearBoxDisplay;
    // Start is called before the first frame update
    private void Awake()
    {
        gearBox = GetComponent<Slider>();
    }



    // Update is called once per frame
    void Update()
    {
        //Debug.Log("slider" + gearBox.value);
        control.RpmLimiter(gearBox.value);
        gearBoxDisplay.text = gearBox.value.ToString();
    }
}
