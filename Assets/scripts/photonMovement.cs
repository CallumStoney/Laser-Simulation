using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class photonMovement : MonoBehaviour
{
    private Slider slider;

    const float SPEED = 299792458f; // Speed of light in meters per second
    const float SPEED_MULTIPLIER = 0.00000005f; // Much slower sub-light speed multiplier for realism
    public Vector3 dir; // Direction of photon movement
    public Rigidbody rb; // Rigidbody for movement
    public Collider photonCollider; // Collider of the photon (for detecting collision with electron collider)

    public float energyLevel = 0f;

    void Start()
    {
        //set values
        slider = GameObject.Find("FrequencySlider").GetComponent<Slider>();
        AssignEnergylevel(1001 - slider.value);
    }

    void Update()
    {

        // Move the photon based on its direction and speed
        rb.velocity = dir * SPEED * SPEED_MULTIPLIER;

        // Raycast to check if photon intersects any electron collider (assuming photon collider is interacting with electron's collider)
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
            {
                // Check if the photon hits an electron (Electron_Circle collider)
                if (hit.collider.CompareTag("Electron"))
                {

                   GameObject AtomS = hit.collider.gameObject.transform.parent.gameObject; //parent object

                   AtomS.GetComponent<AtomAbsorbPhoton>().ElectronSwitch(hit.collider.gameObject, this.gameObject);


                }
            } 

        if (Mathf.Abs(transform.position.x) > 500 || Mathf.Abs(transform.position.y) > 500 || Mathf.Abs(transform.position.z) > 500)
        {
            Destroy(this.gameObject);
        }
    }


    private void AssignEnergylevel(float wavelength)
    {
        // Ensure wavelength is within a valid range to prevent division by zero
        if (wavelength < 1f || wavelength > 1000f)
        {
            return;
        }

        // Energy in electronvolts
        energyLevel = 1240f / wavelength;

        // You can now use energyEV as needed in your script
        //Debug.Log("Energy in eV: " + energyLevel);
    }
}