using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron_Circle : MonoBehaviour
{
    GameObject Nucleus; // Reference to the nucleus (if needed for positioning)
    public int n = 0; // Atomic number
    public GameObject orbital; // Orbital object corresponding to the electron
    MeshFilter orbitalMeshFilter; // Mesh filter of the orbital
    public Collider electronCollider; // Collider for the electron (to be used for photon collision)

    public float spinValue;
    
    

    void Start()
    {
        orbital = GetComponentInParent<SpawnElectrons>().orbitals[n - 1]; // Get orbital corresponding to atomic number n
        orbitalMeshFilter = orbital.GetComponent<MeshFilter>(); // Get the mesh filter of the orbital
        electronCollider = GetComponent<Collider>(); // Get the collider attached to the electron (should be a sphere or similar)
        //Movement();
    }

    void FixedUpdate()
    {
        Movement(); // Move the electron throughout the orbital
    }

    void Movement() 
    {
        Vector3 randomPosition = GetRandomPointInMeshBounds(); // Get a random position within the orbital bounds
        transform.position = randomPosition; // Update position of the electron
    }

    private Vector3 GetRandomPointInMeshBounds()
    {
        Mesh mesh = orbitalMeshFilter.mesh; // Get the mesh of the orbital
        MeshCollider orbitalMeshCollider = orbitalMeshFilter.GetComponent<MeshCollider>(); // Get the mesh collider of the orbital
        Bounds bounds = mesh.bounds; // Get the bounds of the orbital mesh
        const int maxAttempts = 100; // Limit the number of attempts to find a valid point
        int attempts = 0; // Initialize attempt counter

        while (attempts < maxAttempts)
        {
            attempts++; // Increment attempt counter

            // Generate a random point within the bounds of the mesh
            Vector3 randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );

            // Transform point from local to world space
            randomPoint = orbitalMeshFilter.transform.TransformPoint(randomPoint);

            // Check if the point is inside the mesh collider
            if (IsInCollider(orbitalMeshCollider, randomPoint))
            {
                return randomPoint; // Return valid point
            }
        }
        return Vector3.zero; // Return zero if no valid point found
    }

    bool IsInCollider(Collider other, Vector3 point)
    {
        // Check if the point is inside the collider
        if (other.ClosestPoint(point) == point)
        {
            return true; // Point is inside the collider
        }

        return false; // Point is outside
    }
}