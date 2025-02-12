using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtom : MonoBehaviour
{
    public GameObject atom;
    void Start()
    {
        SpawnNumAtoms(30);
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
}
