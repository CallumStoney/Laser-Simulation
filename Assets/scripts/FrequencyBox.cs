using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FrequencyBox : MonoBehaviour
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
            float value = 1001f - GameObject.Find("FrequencySlider").GetComponent<Slider>().value;
            box.text = value.ToString() + "nm";
        }
    }

    void OnInputEnd(string input)
    {
        // Remove % if user re-edits
        string cleaned = input.Replace("nm", "");

        // Try parsing, clamp to 1–100
        if (int.TryParse(cleaned, out int value))
        {
            value = Mathf.Clamp(value, 1, 1000);
            box.text = value.ToString() + "nm";
        }
        else
        {
            // Reset to default if invalid
            box.text = "1000nm";
        }

        //update object 
        GameObject.Find("FrequencySlider").GetComponent<Slider>().value = 1001f - (float.Parse(input));
    }
}
