using UnityEngine;

public class OrbitalView : MonoBehaviour
{
    public Material baseMaterial; // The base material to clone
    private Material instanceMaterial; // The unique material instance for this object
    private Transform parentTransform;

    void Start()
    {
        // Clone the base material to create a unique instance
        instanceMaterial = new Material(baseMaterial);
        GetComponent<Renderer>().material = instanceMaterial;

        // Assume this script is attached to a child object
        parentTransform = transform.parent;
    }

    void Update()
    {
        if (parentTransform != null && instanceMaterial != null)
        {
            Vector3 parentPos = parentTransform.position;
            instanceMaterial.SetVector("_ParentPosition", new Vector4(parentPos.x, parentPos.y, parentPos.z, 0));
        }
    }
}