using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDMovement : MonoBehaviour
{
    float speed = 5f; //base movement speed
    Vector3 mouseRotation;
    float turnSpeed = 0.1f;
    Vector3 currentTurn = new Vector3(0f, 0f, 0f);
    Vector3 displayTurn = new Vector3(0f, 0f, 0f);
    Vector3 initalTurn = Vector3.zero;

    void Start()
    {
        
    }

    void Update()
    {
        //W/A/S/D
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed, Space.Self);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self);
        }


        //rotation
        mouseRotation = Input.mousePosition;
        if (Input.GetMouseButton(1))
        {

            currentTurn = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
            
            displayTurn = new Vector3(displayTurn.x, displayTurn.y - (initalTurn.y - currentTurn.y), displayTurn.z);

            transform.rotation = Quaternion.Euler(displayTurn.x, displayTurn.y, displayTurn.z);
            initalTurn = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        }
        else 
        {
            initalTurn = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
        }
    }
}
