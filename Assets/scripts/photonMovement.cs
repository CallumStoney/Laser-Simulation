using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class photonMovement : MonoBehaviour
{
    const float SPEED = 299792458f; // Speed of light in meters per second
    const float SPEED_MULTIPLIER = 0.0000005f; // Much slower sub-light speed multiplier for realism
    public Vector3 direction; // Direction of photon movement
    public Rigidbody rb; // Rigidbody for movement
    public Collider photonCollider; // Collider of the photon (for detecting collision with electron collider)

    void Start()
    {
        // Randomly set photon direction (normalized) to simulate random emission
        direction = Random.onUnitSphere; // Random direction in 3D space
        rb.velocity = direction * SPEED * SPEED_MULTIPLIER; // Set initial velocity
    }

    void Update()
    {
        // Move the photon based on its direction and speed
        rb.velocity = direction * SPEED * SPEED_MULTIPLIER * Time.deltaTime;

        // Raycast to check if photon intersects any electron collider (assuming photon collider is interacting with electron's collider)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
        {
            // Check if the photon hits an electron (Electron_Circle collider)
            if (hit.collider.CompareTag("Electron"))
            {
                // Photon hits the electron, simulate absorption
                AbsorbPhoton();
            }
        }
    }

    // Simulate photon absorption (can add more behavior here)
    void AbsorbPhoton()
    {
        // Handle photon absorption logic (e.g., trigger electron excitation)
        Debug.Log("Photon absorbed by electron at position: " + transform.position);

        // Destroy photon after absorption
        Destroy(this.gameObject);
    }
}