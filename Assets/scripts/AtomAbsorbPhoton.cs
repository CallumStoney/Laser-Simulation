using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomAbsorbPhoton : MonoBehaviour
{
    //REMINDER: the oture orbital is turned on right now, make sure to fix that to turn it off


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
        // Determine if photon should be absorbed
        if(false)
        {

            //get reference to all the electrons in the atom
            SpawnElectrons atom = this.GetComponent<SpawnElectrons>();

            //atom.electrons;

            Destroy(photon.gameObject);
            Debug.Log("Photon has been absorbed");
        }
        else
        {

        }

    }
}
