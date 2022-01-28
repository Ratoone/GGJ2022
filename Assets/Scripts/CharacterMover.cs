using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 movingDirection;
    private bool keyPressed = false;

    public float forwardSpeed = 5.0f;


    // Start is called before the first frame update
    void Start()
    {
        movingDirection = new Vector3(1, 0, 0);
        controller = gameObject.AddComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newDirection = new Vector3(Math.Sign(Input.GetAxis("Horizontal")), 0, Math.Sign(Input.GetAxis("Vertical")));
        if (newDirection == Vector3.zero) {
            keyPressed = false;
        }
        if (!keyPressed && Math.Abs(newDirection[0]) + Math.Abs(newDirection[2]) == 1) {
            movingDirection = this.transform.TransformDirection(newDirection);
            keyPressed = true;
        }

        controller.Move(movingDirection * forwardSpeed * Time.deltaTime * forwardSpeed);

        if (movingDirection != Vector3.zero)
        {
            gameObject.transform.forward = movingDirection;
        }

    }
}
