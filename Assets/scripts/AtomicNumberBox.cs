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
}