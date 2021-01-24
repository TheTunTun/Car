using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class camera : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform thirdPrsonobjectfollow; //the car
    //public Transform firstPersonObjectfollow;
    public Vector3 thirdPersonOffset;

    private enum cameraState
    {
        firstPerson,
        thirdPerson,
        stand,
        customize,
    }

    private cameraState state;
    private cameraState resumeState;
    
    public float followSpeed = 10;
    public float lookSpeed = 10;

    [SerializeField] private Camera customizeCamera;
    [SerializeField] private Transform firstPersonPosition;
    [SerializeField] private Text cameraText;
    [SerializeField] private Text standText;
    [SerializeField] private Transform thirdPersonPosition;
    [SerializeField] private Transform customizeCameraPosition;

    [SerializeField] private CarCustomization carCustomization;

    private void Awake()
    {
        state = cameraState.thirdPerson;
        
    }

    public void LookAtTarget(Transform objectFollow)
    {
        Vector3 lookDirection = objectFollow.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(lookDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, lookSpeed * Time.deltaTime); //current rotation position to the car rotation.


    }

    public void MoveToTarget(Transform objectFollow, Vector3 offset)
    {
        Vector3 targetPos = objectFollow.position +
                            objectFollow.forward * offset.z +
                            objectFollow.right * offset.x +
                            objectFollow.up * offset.y;
        //Debug.Log(targetPos);

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);// go between one value to another
   
    }

    public void MoveToTarget()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        
    }

    
    void Start()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            SceneManager.LoadScene(0);
        }

    }
    public void ChangeCamera()
    {
        if(state == cameraState.thirdPerson)
        {
            state = cameraState.firstPerson;
        }else if(state == cameraState.firstPerson)
        {
            state = cameraState.thirdPerson;
        }
        else if(state == cameraState.stand)
        {
            state = cameraState.thirdPerson;
        }
    }
    public void ChangeStandMode()
    {

        if(state != cameraState.stand) { state = cameraState.stand; }
        else { state = cameraState.thirdPerson; }
        
    }

    public void ChangeCustomizeCamera()
    {
        resumeState = state;
        state = cameraState.customize;
        



    }

    public void ResumeCamera()
    {
        state = resumeState;
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {

        switch (state)
        {
            case cameraState.thirdPerson:

                cameraText.text = "3rd person";
                standText.text = "Stand Mode Off";

                transform.parent = thirdPersonPosition;


                LookAtTarget(thirdPrsonobjectfollow);
                MoveToTarget(thirdPrsonobjectfollow, thirdPersonOffset);

                GetComponent<Camera>().fieldOfView = 60;

                break;

            case cameraState.firstPerson:

                cameraText.text = "1st person";
                standText.text = "Stand Mode Off";

                transform.parent = firstPersonPosition;

                MoveToTarget();

                GetComponent<Camera>().fieldOfView = 45;

                break;

            case cameraState.stand:

                standText.text = "Stand Mode On";
                transform.parent = thirdPersonPosition;
                LookAtTarget(thirdPrsonobjectfollow);
                GetComponent<Camera>().fieldOfView = 60;

                break;
            case cameraState.customize:

                transform.parent = customizeCameraPosition;
                MoveToTarget();

                break;

        }

    }
}
