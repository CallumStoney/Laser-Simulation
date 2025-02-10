using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElectrons : MonoBehaviour
{
    public float numElectrons = 0f; //number of electrons
    public List<GameObject> orbitals; //list to hold orbital objects
    List<GameObject> electrons; //list to hold electron objects

    public GameObject electron; //reference to the electron prefab
    public GameObject pOrbital; //reference to the orbital prefab
    public GameObject SOrbital; //reference to the orbital prefab
    public GameObject d1Orbital; //reference to the orbital prefab
    public GameObject d2Orbital; //reference to the orbital prefab

    void Start()
    {
        orbitals = new List<GameObject>(); //initialize the orbitals list

        electrons = new List<GameObject>(); //initialize the electrons list

        float amountOfOrbitals = AmountOfOrbitals(numElectrons);

        //instantiate orbitals based on the number of electrons
        for (int i = 1; i <= amountOfOrbitals; i++)
        {
            GameObject orbitalAdd = null;
            if (i == 1)
            {
                orbitalAdd = Instantiate(SOrbital, new Vector3(0, 0, 0), Quaternion.identity); //create an orbital at position (0,0,0)
            }
            if (i == 2)
            {
                orbitalAdd = Instantiate(SOrbital, new Vector3(0, 0, 0), Quaternion.identity); //create an orbital at position (0,0,0)
                orbitalAdd.transform.localScale = new Vector3(4, 4, 4);
            }
            if (i == 3)
            {
                orbitalAdd = Instantiate(pOrbital, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0)); //create an orbital at position (0,0,0)
            }
            if (i == 4)
            {
                orbitalAdd = Instantiate(pOrbital, new Vector3(0, 0, 0), new Quaternion(90, 90, 90, 90)); //create an orbital at position (0,0,0)
            }
            if (i == 5)
            {
                orbitalAdd = Instantiate(pOrbital, new Vector3(0, 0, 0), new Quaternion(270, 270, 360, -360)); //create an orbital at position (0,0,0)
            }
            if (i == 6)
            {
                orbitalAdd = Instantiate(d1Orbital, new Vector3(0, 0, 0), new Quaternion(270, 270, 360, -360)); //create an orbital at position (0,0,0)
            }
            if (i == 7)
            {
                orbitalAdd = Instantiate(d1Orbital, new Vector3(0, 0, 0), new Quaternion(270, 270, 360, -360)); //create an orbital at position (0,0,0)
            }
            if (i == 8)
            {
                orbitalAdd = Instantiate(d2Orbital, new Vector3(0, 0, 0), new Quaternion(270, 270, 360, -360)); //create an orbital at position (0,0,0)
            }
            if (i == 9)
            {
                orbitalAdd = Instantiate(d1Orbital, new Vector3(0, 0, 0), new Quaternion(270, 270, 360, -360)); //create an orbital at position (0,0,0)
            }
            if (i == 10)
            {
                orbitalAdd = Instantiate(d1Orbital, new Vector3(0, 0, 0), new Quaternion(270, 270, 360, -360)); //create an orbital at position (0,0,0)
            }
            orbitalAdd.name = "orbital " + i;
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
        int assignOrbital = 1;
        for (int i = 0; i < numElectrons; i++)
        {
            if (assignOrbital == amountOfOrbitals + 1)
                assignOrbital = 1;
            electrons[i].GetComponent<Electron_Circle>().n = assignOrbital; //assign the electron to its corresponding orbital
            assignOrbital++;

        }
    }

    void Update()
    {
        
    }

    private float AmountOfOrbitals(float electrons)
    {
        //Hunds law
        float num = 0;
        if (electrons <= 2)
            num = 1;
        else if (electrons <= 4)
            num = 2;
        else if (electrons == 5)
            num = 3;
        else if (electrons == 6)
            num = 4;
        else if (electrons <= 10)
            num = 5;
        else if (electrons <= 12)
            num = 6;
        else if (electrons == 13)
            num = 7;
        else if (electrons == 14)
            num = 8;
        else if (electrons <= 18)
            num = 9;
        else if (electrons <= 20)
            num = 10;
        else if (electrons == 21)
            num = 11;
        else if (electrons == 22)
            num = 12;
        else if (electrons == 23)
            num = 13;
        else if (electrons == 24)
            num = 14;
        else if (electrons <= 30)
            num = 15;

        return num;
    }
}