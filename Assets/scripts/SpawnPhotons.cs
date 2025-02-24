using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPhotons : MonoBehaviour
{
    public GameObject photon;
    void Start()
    {
        //SpawnNumPhotonsDirectionPosition(30, new Vector3(0,0,0), Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnNumPhotonsDirectionPosition(1, new Vector3(0, 0, 0), new Vector3(1f,1f,10f));
        SpawnNumPhotonsDirectionPosition(1, new Vector3(0, 0, 0), new Vector3(1f, 0f, 10f));
    }

    private void SpawnNumPhotonsDirectionPosition(int numPhotons, Vector3 position, Vector3 direction)
    {
        for (int i = 0; i < numPhotons; i++)
        {
            GameObject newPhoton = Instantiate(photon, position, Quaternion.identity);
            newPhoton.GetComponent<photonMovement>().direction = direction;
        }
    }
}
