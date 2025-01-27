using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElectrons : MonoBehaviour
{
    public float numElectrons = 0f; //number of electrons
    public List<GameObject> orbitals; //list to hold orbital objects
    List<GameObject> electrons; //list to hold electron objects

    public GameObject electron; //reference to the electron prefab
    public GameObject orbital; //reference to the orbital prefab

    void Start()
    {
        orbitals = new List<GameObject>(); //initialize the orbitals list

        electrons = new List<GameObject>(); //initialize the electrons list

        //instantiate orbitals based on the number of electrons
        for (int i = 0; i < ((int)numElectrons + 1) / 2; i++)
        {
            GameObject orbitalAdd = Instantiate(orbital, new Vector3(0, 0, 0), Quaternion.identity); //create an orbital at position (0,0,0)
            orbitalAdd.transform.SetParent(this.transform); //set orbital as child of the nucleus
            orbitals.Add(orbitalAdd); //add orbital to the list
        }

        //add electrons to list and create them
        for (int i = 0; i < numElectrons; i++)
        {
            GameObject e = Instantiate(electron, new Vector3(0, 0, 0), Quaternion.identity); //create an electron at position (0,0,0)
            e.transform.SetParent(this.transform); //set electron as child of the nucleus
            electrons.Add(e); //add electron to the list
        }

        //change position of electrons
        for (int i = 1; i <= electrons.Count; i++)
        {
            electrons[i - 1].GetComponent<Electron_Circle>().n = (i + 1) / 2; //assign the electron to its corresponding orbital
        }
    }

    void Update()
    {
        
    }
}