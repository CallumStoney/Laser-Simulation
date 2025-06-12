using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.UIElements;
using System.Collections;

public class AtomAbsorbPhoton : MonoBehaviour
{
    private float energyLevel = 0f;
    private float timer = 0.5f;
    private float timeToRelease = 0f;
    private bool time = false;
    public Vector3 dir;

    public GameObject photon;


    private struct ExcitationResult
    {
        public int finalN;
        public float deltaE;
    }

    // Explicit full initialization for Unity compatibility
    private static readonly Dictionary<int, List<(float, int)>> excitationData = new Dictionary<int, List<(float, int)>>()
    {
        { 1, new List<(float, int)> { (10.20f, 2), (12.10f, 3) } },
        { 2, new List<(float, int)> { (21.20f, 2) } },
        { 3, new List<(float, int)> { (1.85f, 2), (3.54f, 3) } },
        { 4, new List<(float, int)> { (4.52f, 2), (5.28f, 3) } },
        { 5, new List<(float, int)> { (4.39f, 2), (6.75f, 3) } },
        { 6, new List<(float, int)> { (7.48f, 2), (11.26f, 3) } },
        { 7, new List<(float, int)> { (7.00f, 2), (10.30f, 3) } },
        { 8, new List<(float, int)> { (7.45f, 2), (10.99f, 3) } },
        { 9, new List<(float, int)> { (12.73f, 2) } },
        { 10, new List<(float, int)> { (16.85f, 2) } },
        { 11, new List<(float, int)> { (2.10f, 3), (3.19f, 4) } },
        { 12, new List<(float, int)> { (4.35f, 3), (6.12f, 4) } },
        { 13, new List<(float, int)> { (3.14f, 4) } },
        { 14, new List<(float, int)> { (4.90f, 4) } },
        { 15, new List<(float, int)> { (5.46f, 4) } },
        { 16, new List<(float, int)> { (6.17f, 4) } },
        { 17, new List<(float, int)> { (7.07f, 4) } },
        { 18, new List<(float, int)> { (11.55f, 4) } },
        { 19, new List<(float, int)> { (1.61f, 4) } },
        { 20, new List<(float, int)> { (2.93f, 4) } },
        { 21, new List<(float, int)> { (2.54f, 4) } },
        { 22, new List<(float, int)> { (2.95f, 4) } },
        { 23, new List<(float, int)> { (2.60f, 4) } },
        { 24, new List<(float, int)> { (2.45f, 4) } },
    };

    public void ElectronSwitch(GameObject atom, GameObject photon, bool isExcited)
    {
        float energyGiven = photon.GetComponent<photonMovement>().energyLevel;
        int atomNumber = atom.GetComponent<SpawnElectrons>().electrons.Count;

        ExcitationResult result = GetExcitation(atomNumber, energyGiven);
        if (result.finalN > 0 && !isExcited)
        {
            //excite (make the atom start geeking)
            atom.GetComponent<AtomMove>().excite = true;

            //copy photon & destroy
            energyLevel = photon.GetComponent<photonMovement>().energyLevel;
            Destroy(photon);

            //set timer till release
            time = true;
        }
        else if (result.finalN > 0 && isExcited)
        {

            //copy photon & destroy
            energyLevel = photon.GetComponent<photonMovement>().energyLevel;
            dir = photon.GetComponent<photonMovement>().dir;
            Destroy(photon);

            //get rid of time
            timeToRelease = 0f;
            time = false;
            ReleaseWhileExcited();
        }
    }


    private ExcitationResult GetExcitation(int atomicNumber, float energyGiven)
    {
        if (excitationData.TryGetValue(atomicNumber, out var transitions))
        {
            foreach (var pair in transitions)
            {
                // Compare up to 2 decimal places
                if (Mathf.Round(pair.Item1 * 100f) == Mathf.Round(energyGiven * 100f))
                {
                    return new ExcitationResult { deltaE = pair.Item1, finalN = pair.Item2 };
                }
            }
        }
        return new ExcitationResult { deltaE = 0f, finalN = 0 };
    }

    private void FixedUpdate()
    {
        if (time)
        {
            timeToRelease += Time.deltaTime;
        }
        if (timeToRelease >= timer)
        {
            timeToRelease = 0f;
            time = false;
            Release();
        }
    }

    private void Release()
    {
        // release photon
        gameObject.GetComponent<AtomMove>().excite = false;

        // instantiate new photon
        GameObject newPhoton = Instantiate(photon, gameObject.transform.position, Quaternion.identity);

        // assign a random direction
        Vector3 randomDirection = new Vector3(0, 0, 1);     //UnityEngine.Random.onUnitSphere; // random normalized direction in 3D
        newPhoton.GetComponent<photonMovement>().dir = randomDirection;

        // set energy level
        newPhoton.GetComponent<photonMovement>().energyLevel = energyLevel;
        energyLevel = 0f;
    }

    private void ReleaseWhileExcited()
    {
        // release photon
        gameObject.GetComponent<AtomMove>().excite = false;

        // instantiate first photon
        GameObject newPhoton = Instantiate(photon, transform.position, Quaternion.identity);
        newPhoton.GetComponent<photonMovement>().dir = new Vector3(0, 0, 1); //dir;
        newPhoton.GetComponent<photonMovement>().energyLevel = energyLevel;

        // start coroutine and pass copies of values
        StartCoroutine(SpawnDelayedPhoton(dir, energyLevel));

        // reset after coroutine has values
        energyLevel = 0f;
        dir = Vector3.zero;
    }

    private IEnumerator SpawnDelayedPhoton(Vector3 savedDir, float savedEnergy)
    {
        yield return new WaitForSeconds(0.3f); // delay in seconds

        GameObject newPhoton2 = Instantiate(photon, transform.position, Quaternion.identity);
        newPhoton2.GetComponent<photonMovement>().dir = new Vector3(0, 0, 1);  //savedDir;
        newPhoton2.GetComponent<photonMovement>().energyLevel = savedEnergy;
    }
}