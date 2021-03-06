using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVacuum : MonoBehaviour
{
    public PauseMenu canvas;
    public InkBar inkBar;
    public float tileFillingQuantity;
    public float goalColorThreshold;
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
            flip();
        }
    }

    private void flip() {
        isAbsorbing = !isAbsorbing;
        GetComponentInChildren<Animator>().SetTrigger("Flip");
    }
    
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.transform.tag == "Finish") {
            Color otherColor = other.gameObject.GetComponent<Renderer>().material.GetColor("_EmissionColor");
            Vector4 colorDifference = storedColor - otherColor;
            if (otherColor == Color.white || colorDifference.magnitude < goalColorThreshold) {
                canvas.win();
            }
            else {
                canvas.warning();
            }
            return;
        }

        if (other.gameObject.transform.tag == "Ground"){
            Tile tile = other.gameObject.GetComponent<Tile>(); 
            
            if (!other.gameObject.GetComponent<Renderer>().enabled) {
                if (isAbsorbing) {
                    other.gameObject.SetActive(false);
                }
                else {
                    other.gameObject.GetComponent<Renderer>().enabled = true;
                }
            }

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
                    flip();
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
                    flip();
                }
            }
        }

    }
}
