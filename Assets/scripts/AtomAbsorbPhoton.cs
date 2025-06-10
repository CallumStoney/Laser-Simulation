using System;
using System.Collections.Generic;
using UnityEngine;

public class AtomAbsorbPhoton : MonoBehaviour
{
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

    public void ElectronSwitch(GameObject electron, GameObject photon)
    {
        float energyGiven = photon.GetComponent<photonMovement>().energyLevel;
        SpawnElectrons atom = GetComponent<SpawnElectrons>();
        int atomNumber = atom.electrons.Count;
        int electronNumber = atom.electrons.IndexOf(electron);

        ExcitationResult result = GetExcitation(atomNumber, energyGiven);
        if (result.finalN > 0)
        {
            Debug.Log($"Electron #{electronNumber} (Z={atomNumber}) excited to n={result.finalN} with ΔE = {result.deltaE:F2} eV.");
        }
        else
        {
            Debug.Log($"Photon energy {energyGiven} eV does NOT match any allowed transition for electron #{electronNumber} (Z={atomNumber}).");
        }

        Destroy(photon);
    }

    private ExcitationResult GetExcitation(int atomicNumber, float energyGiven)
    {
        if (excitationData.TryGetValue(atomicNumber, out var transitions))
        {
            foreach (var pair in transitions)
            {
                if (Mathf.Approximately(energyGiven, pair.Item1))
                {
                    return new ExcitationResult { deltaE = pair.Item1, finalN = pair.Item2 };
                }
            }
        }
        return new ExcitationResult { deltaE = 0f, finalN = 0 };
    }
}