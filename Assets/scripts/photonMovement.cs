using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class photonMovement : MonoBehaviour
{
    [Header("Photon Settings")]
    public Vector3 dir;
    public float energyLevel = 0f;

    private const float SPEED = 299792458f;         // Speed of light in m/s
    private const float SPEED_MULTIPLIER = 0.0000004f; // Custom sub-light scale for simulation

    private Rigidbody rb;
    private static Slider frequencySlider;

    private const float MAX_DISTANCE = 500f;

    private float timer = 0.05f;
    private float time = 0f;
    private bool canBeAbsorbed = false;


    void Start()
    {
        // Cache Rigidbody reference
        rb = GetComponent<Rigidbody>();

        // Cache static slider only once
        if (frequencySlider == null)
        {
            GameObject sliderObj = GameObject.Find("FrequencySlider");
            if (sliderObj != null)
            {
                frequencySlider = sliderObj.GetComponent<Slider>();
            }
        }

        if (frequencySlider != null && energyLevel == 0f)
        {
            AssignEnergyLevel(1001f - frequencySlider.value);
            canBeAbsorbed = true;
        }

        // Set initial velocity
        rb.velocity = dir.normalized * SPEED * SPEED_MULTIPLIER;
    }

    void FixedUpdate()
    {
        // Keep photon moving (in case Rigidbody drag/friction slows it)
        rb.velocity = dir.normalized * SPEED * SPEED_MULTIPLIER;

        // Check for intersection with an electron
        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, 1f)) // short ray to reduce performance cost
        {
            if (hit.collider.CompareTag("Electron") && canBeAbsorbed)
            {
                AtomAbsorbPhoton atom = hit.collider.GetComponentInParent<AtomAbsorbPhoton>();
                if (atom != null)
                {
                    atom.ElectronSwitch((hit.collider.gameObject).transform.parent.gameObject, gameObject, (hit.collider.gameObject).transform.parent.gameObject.GetComponent<AtomMove>().excite);
                }
            }
        }

        // Out-of-bounds check
        Vector3 pos = transform.position;
        if (Mathf.Abs(pos.x) > MAX_DISTANCE || Mathf.Abs(pos.y) > MAX_DISTANCE || Mathf.Abs(pos.z) > MAX_DISTANCE)
        {
            Destroy(gameObject);
        }

        if (!canBeAbsorbed)
        {
            time += Time.deltaTime;
        }
        if (time >= timer)
        {
            time = 0f;
            canBeAbsorbed = true;
        }
    }

    private void AssignEnergyLevel(float wavelength)
    {
        // Energy in eV using E = 1240 / λ (wavelength in nm)
        if (wavelength >= 1f && wavelength <= 1000f)
        {
            energyLevel = 1240f / wavelength;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mirror"))
        {
            dir = -dir;
            canBeAbsorbed = false;
        }
    }
}