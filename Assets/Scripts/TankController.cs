using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public TurretAim TurretAim;
    public TankMover TankMover;

    private Turret [] Turrets; //Using array allow the scenario when there are more than one turret on the tank to shoot at the same time

    

    private void Awake()
    {
        if (TankMover == null)
        {   
            TankMover = GetComponentInChildren<TankMover>();
        }

        if (TurretAim == null)
        {
            TurretAim = GetComponentInChildren<TurretAim>();
        }

        if (Turrets == null || Turrets.Length == 0) //Getting the reference for the turrets on the tank
        {
            Turrets = GetComponentsInChildren<Turret>();
        }
    }

    public void HandleShoot()
    {
        foreach (var turret in Turrets) //All the turrets shoots at the same time
        {
            turret.Shoot();
        }
    }

    public void HandleMoveBody(Vector2 movementVector)
    {
        TankMover.Move(movementVector);
    }

    public void HandleTurretMovement(Vector2 pointerPosition)
    {
        TurretAim.Aim(pointerPosition);
    }


}
