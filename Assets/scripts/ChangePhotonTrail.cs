using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePhotonTrail : MonoBehaviour
{
    private TrailRenderer trail;
    private Slider slider;
    private Color baseColor;

    [Range(0f, 0.5f)]
    public float brightnessVariation = 0.2f; // How much brightness varies

    // Start is called before the first frame update
    void Start()
    {
        trail = GetComponent<TrailRenderer>();

        slider = GameObject.Find("FrequencySlider").GetComponent<Slider>();

        if (slider != null)
        {
            baseColor = slider.GetComponent<SliderColorChanger>().currentColor;

            // Apply initial brightness variation once at start
            Color variedColor = GetBrightnessVariedColor(baseColor, brightnessVariation);

            Gradient gradient = new Gradient();
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(variedColor, 0f),
                    new GradientColorKey(variedColor, 1f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 1f)
                }
            );

            trail.colorGradient = gradient;
        }
        else
        {
            //Debug.LogWarning("FrequencySlider not found or missing Slider component.");
        }
    }

    // Returns the base color with brightness shifted up or down once
    private Color GetBrightnessVariedColor(Color baseColor, float variation)
    {
        Color.RGBToHSV(baseColor, out float h, out float s, out float v);

        // Randomly vary brightness once within [-variation, +variation]
        float brightnessOffset = Random.Range(-variation, variation);
        float newV = Mathf.Clamp01(v + brightnessOffset);

        Color variedColor = Color.HSVToRGB(h, s, newV);
        variedColor.a = baseColor.a;
        return variedColor;
    }
}