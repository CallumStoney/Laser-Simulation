using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDMovement : MonoBehaviour
{
    //movement
    float speed = 30f; //base movement speed
    float sprintSpeed = 20f; //"sprint" movement speed
    bool isHeld = false;

    //rotation
    Vector3 mouseRotation;
    float turnSpeed = 0.1f;
    Vector3 currentTurn = new Vector3(0f, 0f, 0f);
    Vector3 displayTurn = new Vector3(0f, 0f, 0f);
    Vector3 initalTurn = Vector3.zero;

    void Start()
    {
        Camera camera = Camera.main;

        currentTurn = new Vector3(camera.transform.rotation.x, camera.transform.rotation.y, camera.transform.rotation.z);
    }

    void Update()
    {

        //W/A/S/D
        isHeld = false;

        if (Input.GetKey(KeyCode.W))
        {
            isHeld = true;
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.forward * Time.deltaTime * sprintSpeed, Space.Self);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isHeld = true;
            transform.Translate(Vector3.back * Time.deltaTime * speed, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.back * Time.deltaTime * sprintSpeed, Space.Self);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            isHeld = true;
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.left * Time.deltaTime * sprintSpeed, Space.Self);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            isHeld = true;
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.right * Time.deltaTime * sprintSpeed, Space.Self);
            }
        }


        //rotation
        mouseRotation = Input.mousePosition;
        if (Input.GetMouseButton(1))
        {
            currentTurn = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            displayTurn = new Vector3(displayTurn.x - (initalTurn.y - currentTurn.y), displayTurn.y - (initalTurn.x - currentTurn.x), 0f);

            transform.rotation = Quaternion.Euler(-displayTurn.x / 5.334f, displayTurn.y / 5.34f, 0f);
            initalTurn = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        }
        else 
        {
            initalTurn = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        }


        //acceleration
        if (isHeld)
        {
            speed += (speed * 0.005f);
            if (speed >= 500f)
            {
                speed = 500f;
            }
            sprintSpeed = speed * 2f;
        }
        else
        {
            speed = 10f;
            sprintSpeed = 20f;
        } 
        
    }
}
