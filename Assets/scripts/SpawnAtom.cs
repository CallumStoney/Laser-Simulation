using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtom : MonoBehaviour
{
    public GameObject atom;           // Prefab of the atom
    public int numElectrons;          // Number of electrons per atom
    public int amountAtoms;           // Total number of atoms to spawn
    public int atomDiameter;          // Distance between each atom along Z
    public float xRandomRange = 20f;  // Max X-axis random offset

    void Start()
    {
        SpawnAtomsInLine(amountAtoms, atomDiameter, transform.position);
    }

    private void SpawnAtomsInLine(int count, int diameter, Vector3 center)
    {
        int placed = 0;

        // Place the first atom at the center (z = 0)
        PlaceAtom(center, numElectrons);
        placed++;

        int step = 1;

        while (placed < count)
        {
            // +Z
            if (placed < count)
            {
                Vector3 pos = center + new Vector3(Random.Range(-xRandomRange, xRandomRange), 0, step * diameter);
                PlaceAtom(pos, numElectrons);
                placed++;
            }

            // -Z
            if (placed < count)
            {
                Vector3 pos = center + new Vector3(Random.Range(-xRandomRange, xRandomRange), 0, -step * diameter);
                PlaceAtom(pos, numElectrons);
                placed++;
            }

            step++;
        }
    }

    private void PlaceAtom(Vector3 position, int numElectrons)
    {
        GameObject newAtom = Instantiate(atom, position, Quaternion.identity, this.transform);
        newAtom.GetComponent<SpawnElectrons>().numElectrons = numElectrons;
    }
}