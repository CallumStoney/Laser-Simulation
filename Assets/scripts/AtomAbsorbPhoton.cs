using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomAbsorbPhoton : MonoBehaviour
{

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
        // Destroy photon after absorption
        Destroy(photon.gameObject);

        //get reference to all the electrons in the atom
        SpawnElectrons atom = this.GetComponent<SpawnElectrons>();

        //atom.electrons;

    }
}
