using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public Transform cameraTransform;

    [SerializeField] float normalSpeed, fastSpeed, moveSpeed, moveTime, zoomingSpeed;


    public Vector3 newPos, zoomAmount, newZoom;

    //Called before the first frame update
    void Start()
    {
        //stops the transform from equaling zero
        newPos = transform.position;
        //newZoom = cameraTransform.localPosition;
    }

    //Update is called once per frame
    void Update()
    {
        HandleMoveInput();
        //HandleScroll();
    }

    void HandleMoveInput()
    {
        //Going to have to test if this bit of code works in a bit and fix it later once I get the script attached to the camera object!!!
        float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");           //This little peece of code is written by JelleWho https://github.com/jellewie
        if (ScrollWheelChange != 0)//If the scrollwheel has changed
        {                                            
            float R = ScrollWheelChange * zoomingSpeed;                         //The radius from current camera
            float PosX = Camera.main.transform.eulerAngles.x + 90;              //Get up and down
            float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);       //Get left to right
            PosX = PosX / 180 * Mathf.PI;                                       //Convert from degrees to radians
            PosY = PosY / 180 * Mathf.PI;                                       //^
            float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                    //Calculate new coords
            float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);                    //^
            float Y = R * Mathf.Cos(PosX);                                      //^
            float CamX = Camera.main.transform.position.x;                      //Get current camera postition for the offset
            float CamY = Camera.main.transform.position.y;                      //^
            float CamZ = Camera.main.transform.position.z;                      //^
            Camera.main.transform.position = new Vector3(CamX + X, CamY + Y, CamZ + Z);//Move the main camera
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = fastSpeed;
        }
        else
        {
            moveSpeed = normalSpeed;
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPos += (transform.forward * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPos += (transform.forward * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPos += (transform.right * -moveSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPos += (transform.right * moveSpeed);
        }

        //makes the panning smooth
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * moveTime);
    }
    void HandleScroll()
    {
        float mPosX = Input.mousePosition.x;
        float mPosY = Input.mousePosition.y;

        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        int scrollDistance = 30;
        if (mousePosX < scrollDistance)
        {
            transform.Translate(Vector3.right * -moveSpeed * Time.deltaTime);
        }

        if (mousePosX >= Screen.width - scrollDistance)
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (mousePosY < scrollDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
            transform.position.z + 1 * -moveSpeed * Time.deltaTime);
        }

        if (mousePosY >= Screen.height - scrollDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,
            transform.position.z + 1 * moveSpeed * Time.deltaTime);
        }
    }
}

