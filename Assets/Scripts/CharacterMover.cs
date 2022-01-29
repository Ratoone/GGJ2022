using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float forwardSpeed = 5.0f;
    public float tileFillingQuantity;

    private CharacterController controller;
    private Vector3 movingDirection;
    private bool keyPressed = false;
    private bool isAbsorbing = true;
    private Color storedColor = Color.white;
    private float storedContainer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        movingDirection = new Vector3(1, 0, 0);
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            isAbsorbing = !isAbsorbing;
        }

        float verticalVelocity = controller.isGrounded || !isAbsorbing ? 0 : -1;

        Vector3 newDirection = new Vector3(Math.Sign(Input.GetAxis("Horizontal")), 0, Math.Sign(Input.GetAxis("Vertical")));
        if (newDirection == Vector3.zero) {
            keyPressed = false;
        }
        if (!keyPressed && Math.Abs(newDirection[0]) + Math.Abs(newDirection[2]) == 1) {
            movingDirection = this.transform.TransformDirection(newDirection);
            keyPressed = true;
        }

        movingDirection.y = verticalVelocity;

        controller.Move(movingDirection * forwardSpeed * Time.deltaTime * forwardSpeed);

        if (movingDirection != Vector3.zero)
        {
            gameObject.transform.forward = new Vector3(movingDirection[0], 0, movingDirection[2]);
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
                tile.setFadingColor(Color.white);
                storedColor = Color.Lerp(storedColor, tile.oldColor, tileFillingQuantity);
                storedContainer += tileFillingQuantity;
                GetComponent<Renderer>().material.SetColor("_Color", storedColor);
            }
            else {
                tile.setFadingColor(storedColor);
                storedContainer -= tileFillingQuantity;
            }
        }

    }
}
