using System.Drawing;
using UnityEngine;

public class Electron_Circle : MonoBehaviour
{
    GameObject Nucleus; //reference to the nucleus
    public int n = 0; //atomic number

    //orbital
    GameObject orbital; //orbital object corresponding to the electron
    MeshFilter orbitalMeshFilter; //mesh filter of the orbital

    void Start()
    {
        orbital = GetComponentInParent<SpawnElectrons>().orbitals[n - 1]; //gets orbital corresponding to atomic number n
        orbitalMeshFilter = orbital.GetComponent<MeshFilter>(); //gets the mesh filter of the orbital
    }

    void Update()
    {
        Movement(); //movement throughout the orbital
    }

    void Movement()
    {
        Vector3 randomPosition = GetRandomPointInMeshBounds(); //gets a random position in the orbital mesh bounds

        transform.position = randomPosition; //updates position
    }

    private Vector3 GetRandomPointInMeshBounds()
    {
        Mesh mesh = orbitalMeshFilter.mesh; //get the mesh of the orbital
        MeshCollider orbitalMeshCollider = orbitalMeshFilter.GetComponent<MeshCollider>(); //get the mesh collider of the orbital
        Bounds bounds = mesh.bounds; //get the bounds of the orbital mesh
        const int maxAttempts = 100; //limit the number of attempts to find a valid point
        int attempts = 0; //initialize attempt counter

        while (attempts < maxAttempts)
        {
            attempts++; //increment attempt counter

            //generate random point within the bounds of the mesh
            Vector3 randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z)
            );

            //transform point from local to world space
            randomPoint = orbitalMeshFilter.transform.TransformPoint(randomPoint);

            //check if the point is inside the mesh
            if (IsInCollider(orbitalMeshCollider, randomPoint))
            {
                return randomPoint; //return valid point
            }
        }
        return Vector3.zero; //return zero if no valid point found
    }

    //By Ymrasu
    bool IsInCollider(Collider other, Vector3 point)
    {
        //check if the point is inside the collider
        if (other.ClosestPoint(point) == point)
        {
            return true; //point is inside
        }

        return false; //point is outside
    }
}