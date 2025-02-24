using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photonMovement : MonoBehaviour
{
   const float SPEED = 299792458f;
   const float MASS = 0f;
   private float speedMultiplier = 0.00005f;
   public Vector3 direction;
   public Rigidbody rb;
    void Start()
    {
        
    }

    void Update()
    {
        rb.velocity = new Vector3(SPEED * Time.deltaTime * speedMultiplier * direction.x, SPEED * Time.deltaTime * speedMultiplier * direction.y, SPEED * Time.deltaTime * speedMultiplier * direction.z);
        if (Mathf.Abs(transform.position.x) > 200 || Mathf.Abs(transform.position.y) > 200 || Mathf.Abs(transform.position.z) > 200)
        {
            Destroy(this.gameObject);
        }
    }
}
