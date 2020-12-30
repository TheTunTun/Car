using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class GearBox : MonoBehaviour 
{
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
        gearBox = GetComponent<Slider>();
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
        gearBoxDisplay.text = gearBox.value.ToString();
        
    }


    
    
}
