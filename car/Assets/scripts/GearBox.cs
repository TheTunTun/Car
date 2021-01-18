using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class GearBox : MonoBehaviour 
{

    [SerializeField] private Slider gearBox;
    [SerializeField] private CarControl1 control;
    [SerializeField] private Text manualDisplay;
    [SerializeField] private Text autoDisplay;
    [SerializeField] private Text gearMode;
    [SerializeField] private Text autoMode;
    [SerializeField] private AudioManagerScript audioManager;
    [SerializeField] private GameObject auto;
    [SerializeField] private GameObject manual;
    

    private enum gearState
    {
        auto,manual,
    }
    private enum autoDirection { forward, reverse }
    private autoDirection currentAutoDirection;

    private gearState currentGearState;

    // Start is called before the first frame update
    private void Awake()
    {
        currentGearState = gearState.manual;
        currentAutoDirection = autoDirection.forward;
        
    }

    void gearOne()
    {
        gearBox.value = 1;
    }

    public void gearUp()
    {
        if(gearBox.value != gearBox.minValue && audioManager.gearChanged == false) { gearBox.value--; }
    }
    public void gearDown()
    {
        if (gearBox.value != gearBox.maxValue && audioManager.gearChanged == false) { gearBox.value++; }
    }

    public void autoGear()
    {
        if(currentAutoDirection == autoDirection.forward)
        {
            if (control.rpmLimitReached > 3)
            {
                control.rpmLimitReached = 0;
                gearDown();
            }
        }
    }

    

    public void ChangeGearState()
    {
        if (currentGearState == gearState.manual) { currentGearState = gearState.auto; gearMode.text = "Auto"; }
        else { currentGearState = gearState.manual; gearMode.text = "Manual"; }
        
    }

    public void ChangeAutoDirection()
    {
        if (currentAutoDirection == autoDirection.forward) { gearBox.value = 0; currentAutoDirection = autoDirection.reverse; autoMode.text = "R"; }
        else if(currentAutoDirection == autoDirection.reverse) { gearOne(); currentAutoDirection = autoDirection.forward; autoMode.text = "F"; }
    }

    void GearBoxDisplay(Text text)
    {
        if (gearBox.value == 0)
        {
            text.text = "R";
            currentAutoDirection = autoDirection.reverse;
        }
        else
        {
            text.text = gearBox.value.ToString();
            currentAutoDirection = autoDirection.forward;
            
        }
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


        switch (currentGearState)
        {
            case gearState.manual:

                manual.SetActive(true);
                auto.SetActive(false);
                GearBoxDisplay(manualDisplay);

                break;

            case gearState.auto:

                auto.SetActive(true);
                manual.SetActive(false);
                GearBoxDisplay(autoDisplay);
                autoGear();
                


                break;
        }
        
    }


    
    
}
