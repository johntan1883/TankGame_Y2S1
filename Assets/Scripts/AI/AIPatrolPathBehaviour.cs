using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AIPatrolPathBehaviour : AIBehaviour
{
    [SerializeField] private PatrolPath patrolPath; 

    [Range(0.1f, 1)]
    [SerializeField] private float arriveDistance = 1f; //This defines how close the tank must get to a patrol point before it considers the point "reached"
    [SerializeField] private float waitTime = 0.5f; //waiting time for the tank before heading to the next point
    [SerializeField] private bool isWaiting = false;
    [SerializeField] private Vector2 currentPatrolTarget = Vector2.zero;

    private bool isInitialized = false;
    private int currentIndex = -1;

    private void Awake()
    {
        if(patrolPath == null)
        {
            patrolPath = GetComponentInChildren<PatrolPath>();
        }
    }

    public override void PerformAction(TankController tank, AIDetector detector)
    {
        if (!isWaiting)
        {
            if (patrolPath.Length < 2) //if there is no patrol points
            {
                return; //tank do nothing
            }

            if (!isInitialized)
            {
                var currentPathPoint = patrolPath.GetClosePathPoint(tank.transform.position); //Get the position for the closest target point
                this.currentIndex = currentPathPoint.Index;
                this.currentPatrolTarget = currentPathPoint.Position;
                isInitialized = true;
            }

            if (Vector2.Distance(tank.transform.position, currentPatrolTarget) < arriveDistance) //If the tank is close enought to the patrol target
            {
                isWaiting = true;   
                StartCoroutine(WaitCoroutine());
                return;
            }

            Vector2 directionToGo = currentPatrolTarget - (Vector2)tank.TankMover.transform.position; //Get the direction to the next patrol point
            var dotProduct = Vector2.Dot(tank.TankMover.transform.up, directionToGo.normalized); //Dot product is a measure of how aligned the tank is with the direction it needs to move

            if (dotProduct < 0.98f)
            {
                var crossProduct = Vector3.Cross(tank.TankMover.transform.up, directionToGo.normalized); //The crossProduct (using Vector3.Cross) determines the direction in which the tank should rotate
                int rotationResult = crossProduct.z >= 0 ? -1 : 1; //If crossProduct.z >= 0, the target is to the right, so the tank should rotate clockwise (rotationResult = -1)
                                                                   //If crossProduct.z < 0, the target is to the left, so the tank should rotate counterclockwise (rotationResult = 1)
                tank.HandleMoveBody(new Vector2(rotationResult, 1));
            }
            else
            {
                tank.HandleMoveBody(Vector2.up);
            }
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(waitTime);
        var nextPathPoint = patrolPath.GetNextPathPoint(currentIndex);
        currentPatrolTarget = nextPathPoint.Position;
        currentIndex = nextPathPoint.Index;
        isWaiting = false;
    }
}
