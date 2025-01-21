
using UnityEngine;

public class Electron_Circle : MonoBehaviour
{
    GameObject Nucleus;
    float Rotationspeed = 600f;
    public float radius = 0f;
    void Start()
    {
       Nucleus = this.transform.parent.gameObject;

        //set position where it should be
        transform.position = new Vector3 (Nucleus.transform.position.x, Nucleus.transform.position.y + radius, Nucleus.transform.position.z);
    }

    
    void Update()
    {
        transform.RotateAround(Nucleus.transform.position, new Vector3(1,0,1).normalized, Rotationspeed * Time.deltaTime / radius);
    }
}
