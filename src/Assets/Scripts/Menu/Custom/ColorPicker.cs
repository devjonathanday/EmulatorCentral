using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    //min-max brightness
    public ColorPickerBar HRange;
    public ColorPickerBar SRange;
    public ColorPickerBar VRange;

    public Image colorPreview;


    void Update()
    {
        colorPreview.color = Color.HSVToRGB(HRange.GetValue(), SRange.GetValue(), VRange.GetValue());
    }

    public string GetColorHex()
    {
        return "#" + ColorUtility.ToHtmlStringRGB(Color.HSVToRGB(HRange.GetValue(), SRange.GetValue(), VRange.GetValue()));
    }

    public Color GetColor()
    {
        return Color.HSVToRGB(HRange.GetValue(), SRange.GetValue(), VRange.GetValue());
    }
}