using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class SliderColorChanger : MonoBehaviour
{
    private double value;
    private Slider slider;
    private Image fillImage;
    public Color currentColor;

    void Start()
    {
        slider = GetComponent<Slider>();
        fillImage = slider.fillRect.GetComponent<Image>();
    }

    void Update()
    {
        //get the value
        value = 1001 - slider.value;
        //get the color
        currentColor = fillImage.color;
        //set color based on value
        fillImage.color = WavelengthToRGB(value);

    }

    private Color WavelengthToRGB(double wavelength)
    {
        // UV Light
        if (wavelength < 380)
            return new Color(0.2f, 0f, 0.4f); // Dark purple

        // Infrared Light
        if (wavelength > 750)
            return new Color(0.4f, 0f, 0f); // Dark red

        double gamma = 0.8;
        double R = 0, G = 0, B = 0;

        if (wavelength >= 380 && wavelength <= 440)
        {
            R = -(wavelength - 440) / (440 - 380);
            G = 0.0;
            B = 1.0;
        }
        else if (wavelength > 440 && wavelength <= 490)
        {
            R = 0.0;
            G = (wavelength - 440) / (490 - 440);
            B = 1.0;
        }
        else if (wavelength > 490 && wavelength <= 510)
        {
            R = 0.0;
            G = 1.0;
            B = -(wavelength - 510) / (510 - 490);
        }
        else if (wavelength > 510 && wavelength <= 580)
        {
            R = (wavelength - 510) / (580 - 510);
            G = 1.0;
            B = 0.0;
        }
        else if (wavelength > 580 && wavelength <= 645)
        {
            R = 1.0;
            G = -(wavelength - 645) / (645 - 580);
            B = 0.0;
        }
        else if (wavelength > 645 && wavelength <= 750)
        {
            R = 1.0;
            G = 0.0;
            B = 0.0;
        }

        // Intensity correction
        double factor = 1.0;
        if (wavelength >= 380 && wavelength <= 420)
        {
            factor = 0.3 + 0.7 * (wavelength - 380) / (420 - 380);
        }
        else if (wavelength > 700 && wavelength <= 750)
        {
            factor = 0.3 + 0.7 * (750 - wavelength) / (750 - 700);
        }

        float r = Mathf.Pow((float)(R * factor), (float)gamma);
        float g = Mathf.Pow((float)(G * factor), (float)gamma);
        float b = Mathf.Pow((float)(B * factor), (float)gamma);

        return new Color(r, g, b);
    }
}
