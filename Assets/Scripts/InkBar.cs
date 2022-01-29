using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InkBar : MonoBehaviour
{
    public Slider inkSlider;
    public Image inkFill;

    public void setInkLevel(float quantity) {
        inkSlider.value = quantity;
    }

    public void setInkColor(Color color) {
        inkFill.color = color;
    }
}
