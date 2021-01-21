using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleControl : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] UnityEvent toggleOffEvent;
    [SerializeField] UnityEvent toggleOnEvent;

    

    private Toggle toggle;

    void Start()
    {
        toggle = GetComponent<Toggle>();
    }

    // Update is called once per frame
    void Update()
    {
        toggle.onValueChanged.AddListener(delegate
        {
            ToggleValueChanged(toggle);
        });
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn == true)
        {

            toggleOnEvent.Invoke();
        }
        if (toggle.isOn == false)
        {
            toggleOffEvent.Invoke();
        }
    }
}
