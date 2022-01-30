using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float forwardSpeed = 5.0f;
    public float turnSpeed = 2f;

    private CharacterController controller;
    private Vector3 movingDirection;
    private int flipAngle = 0;
    
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
            flipAngle = 180 - flipAngle;
        }
        float verticalVelocity = controller.isGrounded || !GetComponent<ColorVacuum>().isAbsorbing ? 0 : -1;

        Vector3 targetRotation = new Vector3(0, 90*Math.Sign(Input.GetAxis("Horizontal")), 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(targetRotation), Time.deltaTime * turnSpeed);

        movingDirection = transform.forward;
        movingDirection.y = verticalVelocity;

        controller.Move(movingDirection * forwardSpeed * Time.deltaTime * forwardSpeed);
    }
}
