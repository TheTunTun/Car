
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarUpsideDownFixer : MonoBehaviour
{
    // Start is called before the first frame update
    
    bool turned = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dotValue = Vector3.Dot(this.transform.up, Vector3.down);
        
        if(dotValue > -0.5)
        {
            turned = true;
        }
        if (turned) { StartCoroutine("delay", 1.5f);  }

    }

    IEnumerator delay(float t)
    {
        yield return new WaitForSeconds(t);
        Reset();
        
        
    }

    public void Reset()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        turned = false;
    }


}
