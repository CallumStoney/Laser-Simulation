using UnityEngine;

public class CreateUniqueShaderInstance : MonoBehaviour
{
    public Material baseMaterial; // The base material to clone
    private Material instanceMaterial; // The unique material instance for this object
    private Transform parentTransform;

    void Start()
    {
        // Clone the base material to create a unique instance 
        instanceMaterial = new Material(baseMaterial);
        GetComponent<Renderer>().material = instanceMaterial;
        Color cloudColor = GetComponent<Renderer>().material.GetColor("_MainColor");
        if(this.name == "orbital 1")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.0f, 0.0f));
        else if(this.name == "orbital 2")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.92f, 0.016f));
        else if(this.name == "orbital 3")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.64f, 0.0f));
        else if (this.name == "orbital 4")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.64f, 0.0f));
        else if (this.name == "orbital 5")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.64f, 0.0f));
        else if (this.name == "orbital 6")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(0.0f, 1.0f, 0.0f));
        else if (this.name == "orbital 7")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(0.0f, 0.95f, 0.0f));
        else if (this.name == "orbital 8")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(0.0f, 0.95f, 0.0f));
        else if (this.name == "orbital 9")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(0.0f, 0.95f, 0.0f));
        else if (this.name == "orbital 10")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(0.0f, 0.0f, 1.0f));
        else if (this.name == "orbital 11")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.0f, 1.0f));
        else if (this.name == "orbital 12")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.0f, 1.0f));
        else if (this.name == "orbital 13")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.0f, 1.0f));
        else if (this.name == "orbital 14")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.0f, 1.0f));
        else if (this.name == "orbital 15")
            GetComponent<Renderer>().material.SetColor("_MainColor", new Color(1.0f, 0.0f, 1.0f));

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