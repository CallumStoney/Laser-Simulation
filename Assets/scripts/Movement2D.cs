using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    //movement
    float speed = 60f; //base movement speed
    float sprintSpeed = 20f; //"sprint" movement speed
    bool isHeld = false;

    //scroll
    public Camera camera; // Reference to the camera
    public float zoomSpeed = 200f; // How fast the camera zooms in and out
    public float scrollCount = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        //W/A/S/D
        isHeld = false;

        if (Input.GetKey(KeyCode.W))
        {
            isHeld = true;
            transform.Translate(Vector3.up * Time.deltaTime * speed, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.up * Time.deltaTime * sprintSpeed, Space.Self);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            isHeld = true;
            transform.Translate(Vector3.down * Time.deltaTime * speed, Space.Self);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(Vector3.down * Time.deltaTime * sprintSpeed, Space.Self);
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

        //zoom in and out of different scenes
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            if (scroll > 0)
            {
                scrollCount++;
            }
            else
            {
                scrollCount--;
            }

            // Ensure the camera stays within the specified distance range
            if (transform.position.z > -10)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
            else if (transform.position.z < -10 || (scroll < 0 && transform.position.z == -10))
            {
                camera.transform.position += camera.transform.forward * scroll * zoomSpeed;
            }
        }
    }
}
