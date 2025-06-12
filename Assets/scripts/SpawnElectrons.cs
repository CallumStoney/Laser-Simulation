using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElectrons : MonoBehaviour
{
    public float numElectrons = 0f; // number of electrons
    public List<GameObject> orbitals; // list to hold orbital objects
    public List<GameObject> electrons; // list to hold electron objects

    public GameObject electron; // reference to the electron prefab
    public GameObject pOrbital; // reference to the p orbital prefab
    public GameObject SOrbital; // reference to the s orbital prefab
    public GameObject d1Orbital; // reference to the first d orbital prefab
    public GameObject d2Orbital; // reference to the second d orbital prefab

    void Start()
    {
        orbitals = new List<GameObject>(); // initialize the orbitals list
        electrons = new List<GameObject>(); // initialize the electrons list

        float amountOfOrbitals = AmountOfOrbitals(numElectrons);

        // Instantiate orbitals based on the number of electrons
        for (int i = 1; i <= amountOfOrbitals; i++)
        {
            GameObject orbitalAdd = null;
            Vector3 pos = transform.position;

            switch (i)
            {
                case 1:
                    orbitalAdd = Instantiate(SOrbital, pos, Quaternion.identity);
                    break;
                case 2:
                    orbitalAdd = Instantiate(SOrbital, pos, Quaternion.identity);
                    orbitalAdd.transform.localScale = new Vector3(8, 8, 8);
                    break;
                case 3:
                    orbitalAdd = Instantiate(pOrbital, pos, Quaternion.Euler(270f, 270f, 360f));
                    break;
                case 4:
                    orbitalAdd = Instantiate(pOrbital, pos, Quaternion.identity);
                    break;
                case 5:
                    orbitalAdd = Instantiate(pOrbital, pos, Quaternion.Euler(90f, 90f, 90f));
                    break;
                case 6:
                    orbitalAdd = Instantiate(SOrbital, pos, Quaternion.identity);
                    orbitalAdd.transform.localScale = new Vector3(18, 18, 18);
                    break;
                case 7:
                    orbitalAdd = Instantiate(pOrbital, pos, Quaternion.Euler(270f, 270f, 360f));
                    orbitalAdd.transform.localScale = new Vector3(4, 4, 4);
                    break;
                case 8:
                    orbitalAdd = Instantiate(pOrbital, pos, Quaternion.identity);
                    orbitalAdd.transform.localScale = new Vector3(4, 4, 4);
                    break;
                case 9:
                    orbitalAdd = Instantiate(pOrbital, pos, Quaternion.Euler(90f, 90f, 90f));
                    orbitalAdd.transform.localScale = new Vector3(4, 4, 4);
                    break;
                case 10:
                    orbitalAdd = Instantiate(SOrbital, pos, Quaternion.identity);
                    orbitalAdd.transform.localScale = new Vector3(32, 32, 32);
                    break;
                case 11:
                    orbitalAdd = Instantiate(d1Orbital, pos, Quaternion.Euler(45f, 90f, 0f));
                    orbitalAdd.transform.localScale = new Vector3(8, 8, 8);
                    break;
                case 12:
                    orbitalAdd = Instantiate(d1Orbital, pos, Quaternion.Euler(45f, 0f, 0f));
                    orbitalAdd.transform.localScale = new Vector3(8, 8, 8);
                    break;
                case 13:
                    orbitalAdd = Instantiate(d2Orbital, pos, Quaternion.Euler(90f, 0f, 0f));
                    orbitalAdd.transform.localScale = new Vector3(8, 8, 8);
                    break;
                case 14:
                    orbitalAdd = Instantiate(d1Orbital, pos, Quaternion.Euler(0f, 45f, 90f));
                    orbitalAdd.transform.localScale = new Vector3(8, 8, 8);
                    break;
                case 15:
                    orbitalAdd = Instantiate(d1Orbital, pos, Quaternion.identity);
                    orbitalAdd.transform.localScale = new Vector3(8, 8, 8);
                    break;
            }

            if (orbitalAdd != null)
            {
                orbitalAdd.name = "orbital " + i;
                orbitalAdd.transform.SetParent(this.transform); // set orbital as child of the nucleus
                orbitals.Add(orbitalAdd); // add orbital to the list
            }
        }

        // Add electrons to list, create them and set their quantum spin value
        for (int i = 0; i < numElectrons; i++)
        {
            GameObject e = Instantiate(electron, Vector3.zero, Quaternion.identity);
            e.transform.SetParent(this.transform); // set electron as child of the nucleus
            electrons.Add(e);
        }

        // Change position of electrons to orbit their assigned orbital
        int assignOrbital = 1;
        for (int i = 0; i < numElectrons; i++)
        {
            if (assignOrbital > amountOfOrbitals)
                assignOrbital = 1;

            electrons[i].GetComponent<Electron_Circle>().n = assignOrbital; // assign the electron to its corresponding orbital
            assignOrbital++;
        }
    }

    void Update()
    {
        // Optional: implement any per-frame updates if necessary
    }

    private float AmountOfOrbitals(float electrons)
    {
        // Hund's rule approximation to number of orbitals based on electron count
        if (electrons <= 2) return 1;
        if (electrons <= 4) return 2;
        if (electrons == 5) return 3;
        if (electrons == 6) return 4;
        if (electrons <= 10) return 5;
        if (electrons <= 12) return 6;
        if (electrons == 13) return 7;
        if (electrons == 14) return 8;
        if (electrons <= 18) return 9;
        if (electrons <= 20) return 10;
        if (electrons == 21) return 11;
        if (electrons == 22) return 12;
        if (electrons == 23) return 13;
        if (electrons == 24) return 14;
        if (electrons <= 30) return 15;
        return 0;
    }
}