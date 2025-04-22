using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPhotons : MonoBehaviour
{
    public GameObject photon;
    public float spawnInterval = 0.01f; // Time interval between spawns (in seconds)
    private float spawnTimer; // Timer to control spawn rate
    private float radius = 17.5f;

    //stop timer
    private float timeCutOff = 5f; // Time interval between spawns (in seconds)
    private float timerCutOff; // Timer to control spawn rate
    private bool stopSpawning = false;

    int counter = 0;

    void Start()
    {
        // Optionally, you could spawn some photons at the start if you like.
        // SpawnNumPhotonsDirectionPosition(30, new Vector3(0,0,0), Vector3.right);

        //set timers
        spawnTimer = spawnInterval;
        timerCutOff = timeCutOff;
    }

    // Update is called once per frame
    void Update()
    {
        // Count down the timer
        spawnTimer -= Time.deltaTime;

        timerCutOff -= Time.deltaTime;


        // Check if it's time to spawn a new photon
        if (spawnTimer <= 0f && !stopSpawning && timerCutOff <= 4 && timerCutOff >= 3) // Check if it's time to spawn a new photon
        {
            for (int i = 0; i < 15; i++)
            {
                SpawnPhotonsShortCut();
            }
            // Reset the timer based on the interval
            spawnTimer = spawnInterval;
        }

        if (timerCutOff <= 0f)
        {
            stopSpawning = true;
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

    private void SpawnPhotonsShortCut()
    {
        // Spawn photons at the desired interval
        Vector3 positionPlacement = new Vector3(Random.Range(transform.position.x - radius, transform.position.x + radius), Random.Range(transform.position.y - radius, transform.position.y + radius), transform.position.z);
        SpawnNumPhotonsDirectionPosition(1, positionPlacement, new Vector3(0, 0, -1));
    }
}