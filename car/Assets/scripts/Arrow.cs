using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform car;
    [SerializeField] private MiniMapScript miniMap;
    Vector3 arrowScale;
    private void Awake()
    {
        arrowScale = this.transform.localScale;
        miniMap.Zoom += enlargeArrow;
        miniMap.UnZoom += normalArrow;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 arrowNewPosition = car.position;//arrow follows car
        arrowNewPosition.y = car.position.y + 1;
        this.transform.position = arrowNewPosition;

        this.transform.rotation = Quaternion.Euler(0f, car.eulerAngles.y - 90, 0f);
    }

    void enlargeArrow()
    {
        this.transform.localScale = arrowScale * 2;
    }

    void normalArrow()
    {
        this.transform.localScale = arrowScale;
    }

}
