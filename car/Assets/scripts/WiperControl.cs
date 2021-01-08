using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WiperControl : MonoBehaviour
{
    [SerializeField] private Transform wiperLeft;
    [SerializeField] private Transform wiperLeftStart;
    [SerializeField] private Transform wiperLeftEnd;

    [SerializeField] private Transform wiperRight;
    [SerializeField] private Transform wiperRightStart;
    [SerializeField] private Transform wiperRightEnd;

    [SerializeField]int wiperDirection = 0;
   
    [SerializeField] private float wiperSpeed = 1;

    [SerializeField] private bool isWiping;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        isWiping = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isWiping) { Wiping(); };

    }

    public void WiperButton()
    {
        if(isWiping == true) { isWiping = false; } else { isWiping = true; }
    }

    void Wiping()
    {
        float rawlerp =+ Time.deltaTime * wiperSpeed;
            float lerp = Mathf.Min(rawlerp, 1);

        if (wiperDirection == 0)
        {
            
            wiperLeft.localRotation = Quaternion.Lerp(wiperLeft.localRotation, wiperLeftEnd.localRotation, lerp);
            wiperRight.localRotation = Quaternion.Lerp(wiperRight.localRotation, wiperRightEnd.localRotation, lerp);
            if (wiperLeft.localRotation == wiperLeftEnd.localRotation && wiperRight.localRotation == wiperRightEnd.localRotation)
            {
                wiperDirection = 1;
            }
        }
        else if (wiperDirection == 1)
        {
            wiperLeft.localRotation = Quaternion.Lerp(wiperLeft.localRotation, wiperLeftStart.localRotation, lerp);
            wiperRight.localRotation = Quaternion.Lerp(wiperRight.localRotation, wiperRightStart.localRotation, lerp);
            if (wiperLeft.localRotation == wiperLeftStart.localRotation && wiperRight.localRotation == wiperRightStart.localRotation)
            {
                wiperDirection = 0;
            }
        }
    }
    
}
