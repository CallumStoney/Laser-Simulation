using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPhotons : MonoBehaviour
{
    public GameObject photon;
    public float spawnInterval = 0.1f; // Time interval between spawns (in seconds)
    private float spawnTimer = 0f; // Timer to control spawn rate

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
            SpawnNumPhotonsDirectionPosition(10, transform.position, Vector3.left);

            // Reset the timer based on the interval
            spawnTimer = spawnInterval;
        }
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