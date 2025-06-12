using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAtom : MonoBehaviour
{
    [Header("Prefabs & References")]
    public GameObject atomPrefab;
    public Collider cylinderCollider;

    [Header("User Settings")]
    public int numElectrons = 1;
    public int numberOfAtoms = 1;
    public float atomDiameter = 46f;

    [Header("Center Point")]
    public Vector3 centerPoint = new Vector3(0, 0, -150);

    private List<GameObject> atoms = new List<GameObject>();

    private int prevNumElectrons = -1;
    private int prevNumberOfAtoms = -1;
    private float prevAtomDiameter = -1f;

    public Slider sliderAtoms;
    public Slider sliderAtomicNumber;

    void Start()
    {
        SpawnAtoms();
    }

    void Update()
    {
        //update values
        numberOfAtoms = (int)sliderAtoms.value;
        numElectrons = (int)sliderAtomicNumber.value;

        if (numElectrons != prevNumElectrons ||
            numberOfAtoms != prevNumberOfAtoms ||
            Mathf.Abs(atomDiameter - prevAtomDiameter) > 0.01f)
        {
            SpawnAtoms();
        }
    }

    void SpawnAtoms()
    {
        if (cylinderCollider == null)
        {
            Debug.LogWarning("Cylinder collider not assigned.");
            return;
        }

        if (numberOfAtoms <= 0)
        {
            Debug.LogWarning("Number of atoms must be positive.");
            return;
        }

        foreach (var atom in atoms)
        {
            if (atom != null)
                Destroy(atom);
        }
        atoms.Clear();

        Bounds bounds = cylinderCollider.bounds;
        float radiusLimit = bounds.extents.x - atomDiameter / 2f;
        float minZ = bounds.min.z + atomDiameter / 2f;
        float maxZ = bounds.max.z - atomDiameter / 2f;

        int N = numberOfAtoms;

        List<float> zPositions = new List<float>();
        if (N == 1)
        {
            zPositions.Add(centerPoint.z);
        }
        else
        {
            float totalLength = maxZ - minZ;
            for (int i = 0; i < N; i++)
            {
                float z = minZ + (totalLength) * i / (N - 1);
                zPositions.Add(z);
            }
        }

        float goldenAngle = 137.5f * Mathf.Deg2Rad;
        int spawnedCount = 0;

        for (int i = 0; i < N; i++)
        {
            float angle = i * goldenAngle;

            float radius;
            if (i == 1)
            {
                // Make 2nd atom fairly close to the center
                radius = atomDiameter * 0.2f;
            }
            else
            {
                radius = Mathf.Min(radiusLimit, Mathf.Sqrt(i) * (atomDiameter * 0.35f));
            }

            float x = centerPoint.x + Mathf.Cos(angle) * radius;
            float y = centerPoint.y + Mathf.Sin(angle) * radius;
            float z = (i == 1) ? centerPoint.z + atomDiameter * 0.25f : zPositions[i];

            Vector3 pos = new Vector3(x, y, z);

            if (!IsPointInsideCollider(cylinderCollider, pos))
            {
                float testRadius = radius;
                bool found = false;
                while (testRadius > 0)
                {
                    testRadius -= 0.5f;
                    x = centerPoint.x + Mathf.Cos(angle) * testRadius;
                    y = centerPoint.y + Mathf.Sin(angle) * testRadius;
                    pos = new Vector3(x, y, z);

                    if (IsPointInsideCollider(cylinderCollider, pos))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    pos = new Vector3(centerPoint.x, centerPoint.y, z);
                    if (!IsPointInsideCollider(cylinderCollider, pos))
                    {
                        Debug.LogWarning($"Cannot find valid position for atom {i}");
                        continue;
                    }
                }
            }

            GameObject newAtom = Instantiate(atomPrefab, pos, Quaternion.identity, transform);

            var electronScript = newAtom.GetComponent<SpawnElectrons>();
            if (electronScript != null)
            {
                electronScript.numElectrons = numElectrons;
            }

            atoms.Add(newAtom);
            spawnedCount++;
        }

        if (spawnedCount < N)
            Debug.LogWarning($"Only spawned {spawnedCount} atoms out of requested {N}");

        prevNumElectrons = numElectrons;
        prevNumberOfAtoms = numberOfAtoms;
        prevAtomDiameter = atomDiameter;
    }

    bool IsPointInsideCollider(Collider col, Vector3 point)
    {
        Vector3 closest = col.ClosestPoint(point);
        return Vector3.Distance(closest, point) < 0.001f;
    }
}