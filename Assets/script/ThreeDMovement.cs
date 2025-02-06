using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDMovement : MonoBehaviour
{
    float speed = 5f; //base movement speed
    Vector3 mouseRotation;

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
            Debug.Log(Screen.width / 2);
            if (transform.position.x > Screen.width / 2)
                Camera.main.transform.rotation = Quaternion.Euler(30, 30, 0);
        }
    }
}
