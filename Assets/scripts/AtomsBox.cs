using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AtomsBox : MonoBehaviour
{
    public TMP_InputField box;

    void Start()
    {
        // Restrict input to integers
        box.contentType = TMP_InputField.ContentType.IntegerNumber;
        box.onEndEdit.AddListener(OnInputEnd);
    }

    void Update()
    {
        if (!box.isFocused)
        {
            float value = GameObject.Find("AtomSlider").GetComponent<Slider>().value;
            box.text = value.ToString() + " atom(s)";
        }
    }

    void OnInputEnd(string input)
    {
        // Remove % if user re-edits
        string cleaned = input.Replace("atom(s)", "");

        // Try parsing, clamp to 1–100
        if (int.TryParse(cleaned, out int value))
        {
            value = Mathf.Clamp(value, 1, 100);
            box.text = value.ToString() + " atom(s)";
        }
        else
        {
            // Reset to default if invalid
            box.text = "1 atom(s)";
        }

        //update object 
        GameObject.Find("AtomSlider").GetComponent<Slider>().value = (float.Parse(input));
    }
}
