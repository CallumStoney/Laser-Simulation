using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtom : MonoBehaviour
{
    public GameObject atom;
    void Start()
    {
        //SpawnNumAtoms(30); //30 max electrons
        SpawnSpecificAtom(24, 20);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void SpawnNumAtoms(int numAtoms)
    {
        for (int i = 1; i < numAtoms; i++)
        {
            GameObject newAtom = Instantiate(atom, new Vector3((i - 1) * (2f * (i - 1)), 0, 0), Quaternion.identity);
            newAtom.GetComponent<SpawnElectrons>().numElectrons = i;
        }
    }

    private void SpawnSpecificAtom(int num, int amount)
    {
        // Calculate how many atoms should be in each dimension (X, Y, Z)
        int numAtomsPerDimension = Mathf.CeilToInt(Mathf.Pow(amount, 1f / 3f));

        // Iterate through all the atoms and place them in a cube shape
        for (int i = 0; i < amount; i++)
        {
            // Calculate the x, y, z position in the grid
            int x = i % numAtomsPerDimension;
            int y = (i / numAtomsPerDimension) % numAtomsPerDimension;
            int z = i / (numAtomsPerDimension * numAtomsPerDimension);

            // Calculate the actual position with 40 units of spacing between atoms
            Vector3 position = new Vector3(x * 40, y * 40, z * 40);

            // Instantiate the atom at the calculated position
            GameObject newAtom = Instantiate(atom, position, Quaternion.identity);

            // Set the number of electrons
            newAtom.GetComponent<SpawnElectrons>().numElectrons = num;
        }
    }
}
