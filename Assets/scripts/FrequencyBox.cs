using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class FrequencyBox : MonoBehaviour
{
    public TMP_InputField box;
    private Slider frequencySlider;

    void Start()
    {
        // Find the slider once
        frequencySlider = GameObject.Find("FrequencySlider").GetComponent<Slider>();

        // Ensure slider whole numbers is off (double-check in editor)
        frequencySlider.wholeNumbers = false;

        // Allow decimal input in input field
        box.contentType = TMP_InputField.ContentType.Standard;
        box.characterValidation = TMP_InputField.CharacterValidation.Decimal;

        box.onEndEdit.AddListener(OnInputEnd);
    }

    void Update()
    {
        if (!box.isFocused)
        {
            float value = 1001f - frequencySlider.value;

            // Debug: check slider value precision
            // Debug.Log("Slider value: " + frequencySlider.value);

            // Display with 1 decimal place
            box.text = value.ToString("0.0", CultureInfo.InvariantCulture) + "nm";
        }
    }

    void OnInputEnd(string input)
    {
        string cleaned = input.Replace("nm", "").Trim();

        if (float.TryParse(cleaned, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
        {
            // Clamp to valid range
            value = Mathf.Clamp(value, 1f, 1000f);

            // Round to 1 decimal place
            value = Mathf.Round(value * 10f) / 10f;

            // Update input box display
            box.text = value.ToString("0.0", CultureInfo.InvariantCulture) + "nm";

            // Update slider value accordingly
            frequencySlider.value = 1001f - value;
        }
        else
        {
            box.text = "1000.0nm";
            frequencySlider.value = 1f;
        }
    }
}