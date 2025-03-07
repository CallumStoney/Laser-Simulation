using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtom : MonoBehaviour
{

    //atom spawning
    public GameObject atom;
    public int numElectrons;
    public int amountAtoms;
    public int atomDiameter;
    public int maxLength;

    void Start()
    {
        // Example: Start position and length
        SpawnSpecificAtom(numElectrons, amountAtoms, atomDiameter, maxLength, transform.position);
    }

    private void SpawnSpecificAtom(int numElectrons, int amount, int diameter, int length, Vector3 startingPosition)
    {
        int numAtomsPerLayer = 9; // A full 3x3 layer contains 9 atoms
        int fullLayers = amount / numAtomsPerLayer; // Number of complete 3x3 layers
        int remainingAtoms = amount % numAtomsPerLayer; // Atoms left after full layers

        int totalLayers = Mathf.Min(fullLayers + (remainingAtoms > 0 ? 1 : 0), length); // Limit layers to 'length'
        int placedAtoms = 0; // Track number of atoms placed

        for (int z = 0; z < totalLayers; z++)
        {
            int atomsInLayer = (z < fullLayers) ? numAtomsPerLayer : remainingAtoms; // Full layers get 9, last layer gets the remaining

            for (int i = 0; i < atomsInLayer; i++)
            {
                int x = i % 3; // X position in 3x3 grid
                int y = i / 3; // Y position in 3x3 grid

                Vector3 position = startingPosition + new Vector3(x * diameter, y * diameter, z * diameter);


                //change depending on atom type
                GameObject newAtom = Instantiate(atom, position, Quaternion.identity, this.transform); // Set parent to this object
                newAtom.GetComponent<SpawnElectrons>().numElectrons = numElectrons;
                //

                placedAtoms++;
                if (placedAtoms >= amount) return; // Stop when all atoms are placed
            }
        }
    }
}