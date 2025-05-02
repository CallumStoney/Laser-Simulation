using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photonMovement : MonoBehaviour
{
    const float SPEED = 299792458f; // Speed of light in meters per second
    const float SPEED_MULTIPLIER = 0.00000005f; // Much slower sub-light speed multiplier for realism
    public Vector3 dir; // Direction of photon movement
    public Rigidbody rb; // Rigidbody for movement
    public Collider photonCollider; // Collider of the photon (for detecting collision with electron collider)


    private bool isabsorbed = false;


    void Start()
    {

    }

    void Update()
    {

        // Move the photon based on its direction and speed
        rb.velocity = dir * SPEED * SPEED_MULTIPLIER;

        // Raycast to check if photon intersects any electron collider (assuming photon collider is interacting with electron's collider)
        if (!isabsorbed)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
            {
                // Check if the photon hits an electron (Electron_Circle collider)
                if (hit.collider.CompareTag("Electron"))
                {
                    // Photon hits the electron, simulate absorption
                    AbsorbPhoton();
                    isabsorbed = true;
                }
            }
        }

        if (Mathf.Abs(transform.position.x) > 500 || Mathf.Abs(transform.position.y) > 500 || Mathf.Abs(transform.position.z) > 500)
        {
            Destroy(this.gameObject);
        }
    }

    // Simulate photon absorption (can add more behavior here)
    void AbsorbPhoton()
    {
        //Print of how many photons have been absorbed
        for (int i = 0; i < 250; i++)
        {
            Debug.Log("Photon has been absorbed");
        }

        // Destroy photon after absorption
        Destroy(this.gameObject);
    }
}