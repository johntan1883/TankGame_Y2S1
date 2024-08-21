using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolStaticBehaviour : AIBehaviour
{
    [SerializeField] private float patrolDelay = 4f; //The time delay between changes in the turret's direction
    [SerializeField] private Vector2 randomDirection = Vector2.zero; //random direction for turret to rotate
    [SerializeField] private float currentPatrolDelay; //A countdown timer that tracks when the next direction change should occur

    private void Awake()
    {
        randomDirection = Random.insideUnitCircle; //this returns a random point inside or on a circle with radius 1.0
    }

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        float angle = Vector2.Angle(tank.TurretAim.transform.right, randomDirection); //this calculates the angle between the current direction of the turret and the 'randomDirection'
        
        if (currentPatrolDelay <= 0 && (angle < 2)) //if the delay timer 'currentPatrolDelay' has run out, and the turrets is almost facing 'angle < 2' the random direction, a new direction is chosen and the delay is reset
        {
            randomDirection = Random.insideUnitCircle;
            currentPatrolDelay = patrolDelay;
        }
        else
        {
            if (currentPatrolDelay > 0) //Countdown timer
            {
                currentPatrolDelay -= Time.deltaTime;
            }
            else //if the patrol delay has expired and the turret isn't already facing the desired direction, the 'tank.HandleTurretMovement' method is called to rotate the turret towards the 'randomDirection'
            {
                tank.HandleTurretMovement((Vector2)tank.TurretAim.transform.position + randomDirection);
            }
        }
    }
}
