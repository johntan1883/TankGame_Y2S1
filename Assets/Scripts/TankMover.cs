using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;

    [SerializeField] private float maxMoveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float accelaration = 70f;
    [SerializeField] private float deccelaration = 50f;
    
    private Vector2 movementVector;
    private float currentSpeed = 0f;
    private float currentForwardDirection = 1f; //To determine the last direction of the tank

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * currentSpeed * currentForwardDirection * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime)); //Rotating the tank
    }

    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;

        CalculateSpeed(movementVector);

        if (movementVector.y > 0)
        {
            currentForwardDirection = 1; //Set the last direction of the tank, moving up
        }
        else if (movementVector.y < 0)
        {
            currentForwardDirection = -1; //Moving down
        }
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0) //When the tank is moving
        {
            currentSpeed += accelaration * Time.deltaTime; //accelarate
        }
        else //When the tank is stop moving
        {
            currentSpeed -= deccelaration * Time.deltaTime; //deccelarate
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0 ,maxMoveSpeed); //Clamping ensures that the currentSpeed value stays within the define range, between '0' and 'maxMoveSpeed'
    }
}
