using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenRadio : MonoBehaviour
{
    // Start is called before the first frame update
    private Toggle toggle;
    [SerializeField]private AudioManagerScript audioManager;
    void Start()
    {
        toggle = GetComponent<Toggle>();
        
    }

    // Update is called once per frame
    void Update()
    {
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });
    }

    void ToggleValueChanged(Toggle toggle)
    {
        if(toggle.isOn == true) { audioManager.playRadio(); Debug.Log("On"); }
        else if(toggle.isOn == false) { audioManager.stopRadio(); Debug.Log("Off"); }
    }
}
