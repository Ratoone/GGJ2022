using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVacuum : MonoBehaviour
{
    public InkBar inkBar;
    public float tileFillingQuantity;
    public bool isAbsorbing {get; private set;} = true;
    private Color storedColor;
    private float storedContainer = 0f;

    private void Start() {
        storedColor = Color.white;    
    }

    // Update is called once per frame
    void Update()
    {
        inkBar.setInkLevel(storedContainer);
        inkBar.setInkColor(storedColor);

        if (Input.GetKeyDown("space")) {
            isAbsorbing = !isAbsorbing;
        }
    }
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.transform.tag == "Ground"){
            Tile tile = other.gameObject.GetComponent<Tile>(); 
            if (tile.isFading()) {
                return;
            }

            if (isAbsorbing) {
                if (tile.oldColor == Color.white) {
                    return;
                }
                storedContainer += tileFillingQuantity;
                if (storedContainer >= 1f) {
                    storedContainer = 1f;
                    isAbsorbing = !isAbsorbing;
                    return;
                }

                tile.setFadingColor(Color.white);
                storedColor = Color.Lerp(storedColor, tile.oldColor, tileFillingQuantity / storedContainer);
                GetComponent<Renderer>().material.SetColor("_Color", storedColor);
            }
            else {
                tile.setFadingColor(storedColor);
                storedContainer -= tileFillingQuantity;
                if (storedContainer <= 0) {
                    storedContainer = 0;
                    isAbsorbing = !isAbsorbing;
                }
            }
        }

    }
}
