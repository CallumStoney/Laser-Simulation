using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomAbsorbPhoton : MonoBehaviour
{

    private bool isExcited = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ElectronSwitch(GameObject electon, GameObject photon)
    {
        isExcited = true;


        // Destroy photon after absorption
        Destroy(photon.gameObject);
    }
}
