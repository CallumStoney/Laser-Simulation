using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffMirrors : MonoBehaviour
{
    public GameObject mirrorBack;
    public Toggle mirrorToggle;  // Assign this in the Inspector

    void Start()
    {
        if (mirrorToggle != null)
        {
            // Set initial state
            mirrorBack.SetActive(mirrorToggle.isOn);

            // Add listener
            mirrorToggle.onValueChanged.AddListener(OnToggleChanged);
        }
    }

    void OnToggleChanged(bool isOn)
    {
        if (mirrorBack != null)
            mirrorBack.SetActive(isOn);
    }
}