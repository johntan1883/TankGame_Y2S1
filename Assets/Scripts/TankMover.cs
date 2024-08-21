using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankMover : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public TankMovementData movementData;

    private Vector2 movementVector;
    private float currentSpeed = 0f;
    private float currentForwardDirection = 1f; //To determine the last direction of the tank

    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();

    private void Awake()
    {
        rb2d = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * currentSpeed * currentForwardDirection * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * movementData.rotationSpeed * Time.fixedDeltaTime)); //Rotating the tank
    }


    public void Move(Vector2 movementVector)
    {
        this.movementVector = movementVector;

        CalculateSpeed(movementVector);

        OnSpeedChange?.Invoke(this.movementVector.magnitude); //passing the speed data to the event

        if (movementVector.y > 0)
        {
            if (currentForwardDirection == -1)
            {
                currentSpeed = 0f;
            }
            currentForwardDirection = 1f;
        }
        else if (movementVector.y < 0)
        {
            if (currentForwardDirection == 1) 
            {
                currentSpeed = 0f;
            }
            currentForwardDirection = -1; //Moving down
        }
    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0) //When the tank is moving
        {
            currentSpeed += movementData.acceleration * Time.deltaTime; //accelarate
        }
        else //When the tank is stop moving
        {
            currentSpeed -= movementData.decceleration * Time.deltaTime; //deccelarate
        }

        currentSpeed = Mathf.Clamp(currentSpeed, 0 ,movementData.maxSpeed); //Clamping ensures that the currentSpeed value stays within the define range, between '0' and 'maxMoveSpeed'
    }
}
