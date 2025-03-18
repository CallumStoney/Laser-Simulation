using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDSpawnElectrons : MonoBehaviour
{
    public float numElectrons = 0f; //number of electrons
    public List<GameObject> paths; //list to hold electron paths
    List<GameObject> electrons; //list to hold electron objects

    public GameObject electron; //reference to the electron prefab
    public GameObject electronPath; //reference to electron path


    void Start()
    {
        paths = new List<GameObject>(); //initialize the orbitals list

        electrons = new List<GameObject>(); //initialize the electrons list

        for (int i = 1; i <= numElectrons; i++)
        {
            GameObject path = Instantiate(electronPath, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            path.transform.localScale = new Vector3(i, i, 1f);

            path.name = "path " + i;
            path.transform.SetParent(this.transform); //set orbital as child of the nucleus
            paths.Add(path); //add orbital to the list
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
