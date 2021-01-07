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

    
    
    public float followSpeed = 10;
    public float lookSpeed = 10;


    [SerializeField] private Transform firstPersonPosition;
    [SerializeField] private Text cameraText;
    [SerializeField] private Text standText;
    [SerializeField] private Transform thirdPersonPosition;
    //[SerializeField] private MeshRenderer carBody;
    //[SerializeField] private GameObject firstPersonCar;

    public bool stand { get; set; }

    public bool thirdPerson { get; set; }

    private void Awake()
    {
        thirdPerson = true;
        stand = false;
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

        if(stand == false)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);// go between one value to another
                                                                                                           //Debug.Log("followspeed"+ followSpeed * followSpeedMultiplyer * Time.deltaTime);
        }
    }

    public void MoveToTarget()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
        
    }

    public void ChangeCamera()
    {
        stand = false;
        if(thirdPerson == true)
        {
            thirdPerson = false;
            
        }
        else
        {
            thirdPerson = true;
            
        }



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

    public void ChangeStandMode()
    {
        thirdPerson = true;
        
        if (stand) { stand = false;  } 
        else { stand = true; }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (thirdPerson)
        {
            cameraText.text = "3rd person";
            transform.parent = thirdPersonPosition;


            LookAtTarget(thirdPrsonobjectfollow);
            MoveToTarget(thirdPrsonobjectfollow, thirdPersonOffset);

            //carBody.enabled = true;
            //firstPersonCar.SetActive(false);
            GetComponent<Camera>().fieldOfView = 60;
        }
        else
        {
            cameraText.text = "1st person";

            transform.parent = firstPersonPosition;

            //carBody.enabled = false;
            //firstPersonCar.SetActive(true);

            MoveToTarget();

            GetComponent<Camera>().fieldOfView = 45;
        }

        if (stand) { standText.text = "Stand Mode On"; }
        else { standText.text = "Stand Mode Off"; }
    }
}
