using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtom : MonoBehaviour
{
    [Header("Cylinder Dimensions")]
    public float cylinderRadius = 25f;      // Radius of cylinder (x,y plane)
    public float cylinderLength = 200f;      // Length along z-axis

    [Header("Atom Settings")]
    public GameObject atomPrefab;             // Prefab of the atom to spawn
    public int numElectrons = 1;              // Number of electrons per atom
    public int amountAtoms = 1000;            // Max atoms to spawn (caps total)
    public int atomDiameter = 46;             // Diameter of each atom sphere

    // Center of cylinder in world space
    public Vector3 cylinderCenter = new Vector3(0, 0, -150);

    void Start()
    {
        SpawnAtomsInCylinder();
    }

    void SpawnAtomsInCylinder()
    {
        float atomRadius = atomDiameter / 2f;

        // Calculate boundaries accounting for atom radius so they don't go outside cylinder
        float halfLength = cylinderLength / 2f;

        float zMin = cylinderCenter.z - halfLength + atomRadius;
        float zMax = cylinderCenter.z + halfLength - atomRadius;

        float yMin = cylinderCenter.y - cylinderRadius + atomRadius;
        float yMax = cylinderCenter.y + cylinderRadius - atomRadius;

        float maxXY = cylinderRadius - atomRadius;

        int spawnedCount = 0;

        // Loop through z (length)
        for (float z = zMin; z <= zMax; z += atomDiameter)
        {
            // Loop through y (height)
            for (float y = yMin; y <= yMax; y += atomDiameter)
            {
                // Loop through x (width)
                for (float x = -maxXY; x <= maxXY; x += atomDiameter)
                {
                    if (spawnedCount >= amountAtoms)
                        return; // reached max atoms

                    // Check if (x,y) is inside cylinder cross-section circle
                    if (x * x + y * y <= maxXY * maxXY)
                    {
                        Vector3 pos = new Vector3(x, y, z);

                        // Instantiate atom at position, parented to this GameObject for neat hierarchy
                        GameObject newAtom = Instantiate(atomPrefab, pos, Quaternion.identity, transform);

                        // Set number of electrons if script exists on prefab
                        var electronScript = newAtom.GetComponent<SpawnElectrons>();
                        if (electronScript != null)
                            electronScript.numElectrons = numElectrons;

                        spawnedCount++;
                    }
                }
            }
        }

        Debug.Log($"Total atoms spawned: {spawnedCount}");
    }
}
