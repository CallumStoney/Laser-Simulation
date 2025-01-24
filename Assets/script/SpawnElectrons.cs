using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnElectrons : MonoBehaviour
{
    public float numElectrons = 0f;
    private List<GameObject> orbitals;
    List<GameObject> electrons;

    public GameObject electron;
    public GameObject orbital;

    void Start()
    {
        orbitals = new List<GameObject>();

        electrons = new List<GameObject>();
        for(int i = 0; i < ((int)numElectrons + 1) / 2; i++)
        {
            orbitals.Add(Instantiate(orbital, new Vector3(0, 0, 0), Quaternion.identity));
            orbitals.A
        }
            
    
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
            electrons[i - 1].GetComponent<Electron_Circle>().n = i;
        }
    }

    void Update()
    {
        
    }
}
