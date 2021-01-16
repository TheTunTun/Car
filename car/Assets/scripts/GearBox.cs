using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class GearBox : MonoBehaviour 
{

    [SerializeField]
    private Slider gearBox;
    [SerializeField]
    private CarControl1 control;
    [SerializeField]
    private Text gearBoxDisplay;
    [SerializeField]
    private AudioManagerScript audioManager;

    

    // Start is called before the first frame update
    private void Awake()
    {
       
    }

    public void gearUp()
    {
        if(gearBox.value != gearBox.minValue && audioManager.gearChanged == false) { gearBox.value--; }
    }
    public void gearDown()
    {
        if (gearBox.value != gearBox.maxValue && audioManager.gearChanged == false) { gearBox.value++; }
    }

    // Update is called once per frame
    void Update()
    {
        if (audioManager.gearChanged)
        {
            gearBox.interactable = false;
        }
        else
        {
            gearBox.interactable = true;
        }

        control.RpmLimiter(gearBox.value);
        if(gearBox.value == 0)
        {
            gearBoxDisplay.text = "R";
        }
        else
        {
            gearBoxDisplay.text = gearBox.value.ToString();
        }
        
        
    }


    
    
}
