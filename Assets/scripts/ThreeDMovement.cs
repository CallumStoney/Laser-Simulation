using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDMovement : MonoBehaviour
{
    // Movement
    float speed = 60f;
    float sprintSpeed = 20f;
    bool isHeld = false;

    // Rotation
    Vector3 displayTurn = Vector3.zero;
    Vector3 initialMousePos = Vector3.zero;

    // Zoom
    public Camera camera;
    public float zoomSpeed = 200f;
    public float scrollCount = 0f;

    void Start()
    {
        if (camera == null) camera = Camera.main;

        Vector3 eulerRotation = transform.eulerAngles;
        displayTurn = new Vector3(-eulerRotation.x * 5.334f, eulerRotation.y * 5.34f, 0f);
    }

    void Update()
    {
        // Movement input
        isHeld = false;
        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDir += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) moveDir += Vector3.back;
        if (Input.GetKey(KeyCode.A)) moveDir += Vector3.left;
        if (Input.GetKey(KeyCode.D)) moveDir += Vector3.right;

        if (moveDir != Vector3.zero)
        {
            isHeld = true;
            float moveMultiplier = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;
            transform.Translate(moveDir.normalized * Time.deltaTime * moveMultiplier, Space.Self);
        }

        // Mouse rotation
        if (Input.GetMouseButtonDown(1))
        {
            initialMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 delta = Input.mousePosition - initialMousePos;
            displayTurn.x += delta.y;  // flipped X-axis
            displayTurn.y += delta.x;  // flipped Y-axis

            transform.rotation = Quaternion.Euler(displayTurn.x / -5.334f, displayTurn.y / 5.34f, 0f);
            initialMousePos = Input.mousePosition;
        }

        // Speed acceleration
        if (isHeld)
        {
            speed += speed * 0.0005f;
            speed = Mathf.Min(speed, 500f);
            sprintSpeed = speed * 2f;
        }
        else
        {
            speed = 10f;
            sprintSpeed = 20f;
        }

        // Zoom
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            scrollCount += scroll > 0 ? 1 : -1;

            if (scrollCount < 5 && scrollCount > -5)
            {
                camera.transform.position += camera.transform.forward * scroll * zoomSpeed;
            }
        }
    }
}