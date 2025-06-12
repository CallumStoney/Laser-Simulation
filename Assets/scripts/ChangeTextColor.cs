using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
    public TextMeshProUGUI myText;
    private Image image;

    void Start()
    {
        // Get the Image component from the same GameObject this script is attached to
        image = GetComponent<Image>();

        if (image == null)
        {
            Debug.LogWarning("No Image component found on this GameObject.");
        }
        if (myText == null)
        {
            Debug.LogWarning("TextMeshProUGUI reference not assigned.");
        }
    }

    void Update()
    {
        if (image != null && myText != null)
        {
            // Set the vertex color of the text to match the Image color
            myText.color = image.color;
        }
    }
}