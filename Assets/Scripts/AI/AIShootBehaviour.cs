using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootBehaviour : AIBehaviour
{
    [SerializeField] private float fieldOfVisionForShooting = 60; 
    
    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if (TargetInFOV(tank, detector))
        {
            tank.HandleMoveBody(Vector2.zero); //stop the tank's movement
            tank.HandleShoot(); //the tank shoots at the target
        }

        tank.HandleTurretMovement(detector.Target.position); //Regardless of whether the target is in the FOV, the tank's turret will rotate to face the target's current position.
    }

    private bool TargetInFOV(TankController tank, AIDetector detector)
    {
        var direction = detector.Target.position - tank.TurretAim.transform.position; //This calculates the direction vector from the tank's turret to the target's position
        if (Vector2.Angle(tank.TurretAim.transform.right, direction) < fieldOfVisionForShooting / 2) //The 'Vector2.Angle' method calculates the angle between the tank's turret's direction and the direction to the target
        {                                                                                            //If this angle is less than half of 'fieldOfVisionForShooting', the target is within the FOV       
            return true;
        }
        return false;
    }
}
