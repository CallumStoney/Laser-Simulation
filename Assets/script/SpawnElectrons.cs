using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElectrons : MonoBehaviour
{
    public float numElectrons = 0f;
    List<GameObject> electrons;

    public GameObject electron;

    void Start()
    {
        electrons = new List<GameObject>();

        //add electrons to list and create them
        for (int i = 0; i < numElectrons; i++)
        {
            GameObject e = Instantiate(electron, new Vector3(0,0,0), Quaternion.identity);
            e.transform.SetParent(this.transform);
            electrons.Add(e);
        }

        //change position of electrons
        for (int i = 1; i <= electrons.Count; i++)
        {
            electrons[i - 1].GetComponent<Electron_Circle>().radius = Mathf.Pow(i,2);
        }
    }

    void Update()
    {
        
    }
}
