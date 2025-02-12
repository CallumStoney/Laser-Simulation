using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photonMovement : MonoBehaviour
{
   const float speed = 299792458f;
   const float mass = 0f;
   private float speedMultiplier = 0.001f;
   public Rigidbody rb;
    void Start()
    {
        
    }

    void Update()
    {
        rb.velocity = new Vector3(speed * Time.deltaTime * speedMultiplier, 0f,0f);
        if (transform.position.x > 200)
        {
            transform.position = new Vector3(0f, 0, 0);
        }
    }
}
