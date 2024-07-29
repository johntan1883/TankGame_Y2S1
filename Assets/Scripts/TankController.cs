using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public Transform turretParent;

    [SerializeField] private float maxMoveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float turretRotationSpeed = 150f;
    private Vector2 movementVector;
    

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb2d.velocity = (Vector2)transform.up * movementVector.y * maxMoveSpeed * Time.fixedDeltaTime;
        rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -movementVector.x * rotationSpeed * Time.fixedDeltaTime));
    }

    public void HandleShoot()
    {
        Debug.Log("Shooting");
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        this.movementVector = movementVector;
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        var turretDirection = (Vector3)pointerPosition - turretParent.position;

        var desiredAngle = Mathf.Atan2(turretDirection.y, turretDirection.x) * Mathf.Rad2Deg;

        var rotationStep = turretRotationSpeed * Time.deltaTime;

        turretParent.rotation = Quaternion.RotateTowards(turretParent.rotation, Quaternion.Euler(0, 0, desiredAngle - 90), rotationStep);
    }


}
