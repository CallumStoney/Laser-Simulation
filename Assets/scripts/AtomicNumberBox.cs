using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AtomicNumberBox : MonoBehaviour
{
    public TMP_InputField box;

    private readonly string[] elementNames = new string[]
    {
        "Hydrogen", "Helium", "Lithium", "Beryllium", "Boron", "Carbon", "Nitrogen", "Oxygen",
        "Fluorine", "Neon", "Sodium", "Magnesium", "Aluminum", "Silicon", "Phosphorus", "Sulfur",
        "Chlorine", "Argon", "Potassium", "Calcium", "Scandium", "Titanium", "Vanadium", "Chromium"
    };

    void Start()
    {
        box.contentType = TMP_InputField.ContentType.IntegerNumber;
        box.onEndEdit.AddListener(OnInputEnd);
    }

    void Update()
    {
        if (!box.isFocused)
        {
            int value = Mathf.Clamp((int)GameObject.Find("AtomicSlider").GetComponent<Slider>().value, 1, 24);
            string element = elementNames[value - 1];
            box.text = value.ToString() + " (" + element + ")";

            //do it here
            GameObject.Find("FrequencySlider").GetComponent<Slider>().value = GetFrequencyRequired((int)GameObject.Find("AtomicSlider").GetComponent<Slider>().value);
        }
    }

    void OnInputEnd(string input)
    {
        // Strip any non-numeric characters (for safety)
        string cleaned = System.Text.RegularExpressions.Regex.Replace(input, @"[^\d]", "");

        if (int.TryParse(cleaned, out int value))
        {
            value = Mathf.Clamp(value, 1, 24);
            string element = elementNames[value - 1];
            box.text = value.ToString() + " (" + element + ")";
            GameObject.Find("AtomicSlider").GetComponent<Slider>().value = value;
        }
        else
        {
            box.text = "1 (Hydrogen)";
            GameObject.Find("AtomicSlider").GetComponent<Slider>().value = 1;
        }
    }

    public float GetFrequencyRequired(int atom)
    {
        float energyEv;

        if (atom == 1) energyEv = 10.20f;
        else if (atom == 2) energyEv = 21.20f;
        else if (atom == 3) energyEv = 1.85f;
        else if (atom == 4) energyEv = 4.52f;
        else if (atom == 5) energyEv = 4.39f;
        else if (atom == 6) energyEv = 7.48f;
        else if (atom == 7) energyEv = 7.00f;
        else if (atom == 8) energyEv = 7.45f;
        else if (atom == 9) energyEv = 12.73f;
        else if (atom == 10) energyEv = 16.85f;
        else if (atom == 11) energyEv = 2.10f;
        else if (atom == 12) energyEv = 4.35f;
        else if (atom == 13) energyEv = 3.14f;
        else if (atom == 14) energyEv = 4.90f;
        else if (atom == 15) energyEv = 5.46f;
        else if (atom == 16) energyEv = 6.17f;
        else if (atom == 17) energyEv = 7.07f;
        else if (atom == 18) energyEv = 11.55f;
        else if (atom == 19) energyEv = 1.61f;
        else if (atom == 20) energyEv = 2.93f;
        else if (atom == 21) energyEv = 2.54f;
        else if (atom == 22) energyEv = 2.95f;
        else if (atom == 23) energyEv = 2.60f;
        else if (atom == 24) energyEv = 2.45f;
        else
            return -1f;

        const float eVtoNm = 1239.841984f;  // updated constant
        float wavelength = eVtoNm / energyEv;
        wavelength = Mathf.Round(wavelength * 10f) / 10f;  // round to 1 decimal place

        return 1001f - wavelength;
    }
}