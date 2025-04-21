using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPhotons : MonoBehaviour
{
    public GameObject photon;
    public float spawnInterval = 0.01f; // Time interval between spawns (in seconds)
    private float spawnTimer = 0f; // Timer to control spawn rate
    private float radius = 17.5f;

    int counter = 0;

    void Start()
    {
        // Optionally, you could spawn some photons at the start if you like.
        // SpawnNumPhotonsDirectionPosition(30, new Vector3(0,0,0), Vector3.right);
    }

    // Update is called once per frame
    void Update()
    {
        // Count down the timer
        spawnTimer -= Time.deltaTime;

        // Check if it's time to spawn a new photon
        if (spawnTimer <= 0f)
        {
            // Spawn photons at the desired interval
            Vector3 positionPlacement = new Vector3(Random.Range(transform.position.x - radius, transform.position.x + radius), Random.Range(transform.position.y - radius, transform.position.y + radius), transform.position.z);
            SpawnNumPhotonsDirectionPosition(1, positionPlacement, new Vector3(0,0,-1));

            // Reset the timer based on the interval
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnNumPhotonsDirectionPosition(int numPhotons, Vector3 position, Vector3 dir)
    {
        for (int i = 0; i < numPhotons; i++)
        {
            Debug.Log("spawn photon");
            GameObject newPhoton = Instantiate(photon, position, Quaternion.identity);
            newPhoton.name = newPhoton.name + counter;
            newPhoton.GetComponent<photonMovement>().dir = dir;
            counter++;
        }
    }
}