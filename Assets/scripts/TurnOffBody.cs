using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnOffBody : MonoBehaviour
{
    public GameObject body;      // Assign your body GameObject in the Inspector
    public Toggle toggle;        // Assign your Toggle in the Inspector

    private MeshRenderer meshRenderer;

    void Start()
    {
        if (body != null)
            meshRenderer = body.GetComponent<MeshRenderer>();

        if (toggle != null)
        {
            toggle.onValueChanged.AddListener(OnToggleChanged);
            OnToggleChanged(toggle.isOn); // Set initial state
        }
    }

    void OnToggleChanged(bool isOn)
    {
        if (meshRenderer != null)
            meshRenderer.enabled = isOn;
    }
}