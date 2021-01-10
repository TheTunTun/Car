using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class turnsignal : MonoBehaviour
{
    [SerializeField]private GameObject signal;
    [SerializeField] private LightControl lightControl;
    float turnOn;
    // Start is called before the first frame update
    void Start()
    {
        
    }




    // Update is called once per frame
    private void FixedUpdate()
    {
        Toggle m_toggle = GetComponent<Toggle>();
        if (m_toggle.isOn)
        {
            lightControl.TurnSignalOn(true, signal);
        }
        else
        {
            lightControl.TurnSignalOn(false, signal);
        }
    }
}
