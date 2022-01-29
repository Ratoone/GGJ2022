using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color targetColor;
    public float fadingTime = 2f;

    public Color oldColor { get; private set;}
    private float fadingProgress = 0f;

    // Update is called once per frame
    void Update()
    {
        if (fadingProgress > 1f) {
            fadingProgress = 0f;
            setInstantColor(targetColor);
            return;
        }

        if (oldColor == targetColor) {
            return;
        }

        setColor(Color.Lerp(oldColor, targetColor, fadingProgress));
        fadingProgress += Time.deltaTime / fadingTime;
    }

    public void setInstantColor(Color color) {
        targetColor = color;
        oldColor = color;
        setColor(color);
    }

    public bool isFading() {
        return fadingProgress > 0f;
    }

    public void setFadingColor(Color color) {
        targetColor = color;
        fadingProgress = 0;
    }

    private void setColor(Color color) {
        GetComponent<Renderer>().material.SetColor("_Color", color);
    }


}
