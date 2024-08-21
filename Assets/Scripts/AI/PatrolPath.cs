using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPath : MonoBehaviour
{
    [SerializeField] private List<Transform> patrolPoints = new List<Transform>(); //Initiaze the list for the patrol points

    public int Length { get => patrolPoints.Count; } //The size of the list

    [Header("Gizmos Parameters")]
    [SerializeField] private Color pointsColor = Color.blue;
    [SerializeField] private float pointSize = 0.3f;
    [SerializeField] private Color lineColor = Color.magenta;

    public struct PathPoint
    {
        public int Index; //index of patrol point in the list
        public Vector2 Position; //2D position of the patrol point
    }

    public PathPoint GetClosePathPoint(Vector2 tankPosition) //This method finds the patrol point closest to the current position of the tank
    {
        var minDistance = float.MaxValue;
        var index = -1;
        for (int i = 0; i < patrolPoints.Count; i++)
        {
            var tempDistance = Vector2.Distance(tankPosition, patrolPoints[i].position);
            if (tempDistance < minDistance)
            {
                minDistance = tempDistance;
                index = i;
            }
        }

        return new PathPoint { Index = index, Position = patrolPoints[index].position }; //return the closest patrol point as a 'PatrolPoint' struct containing its index and position
    }

    public PathPoint GetNextPathPoint(int index) //This method calculates the next patrol point based on the given index
    {
        var newIndex = index + 1 >= patrolPoints.Count ? 0 : index + 1; //If the current point is the last one, it loops back to the first point
        return new PathPoint { Index = newIndex, Position = patrolPoints[newIndex].position };  //Returns the next patrol point as a 'PathPoint' struct
    }

    private void OnDrawGizmos() //Visualize the paath patrol
    {
        if (patrolPoints.Count == 0)
        {
            return;
        }

        for (int i = patrolPoints.Count - 1; i >= 0; i--)
        {
            if (i == -1 || patrolPoints[i] == null)
            {
                return;
            }

            Gizmos.color = pointsColor;
            Gizmos.DrawSphere(patrolPoints[i].position, pointSize);

            if (patrolPoints.Count == 1 || i == 0)
            {
                return;
            }

            Gizmos.color = lineColor;
            Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i - 1].position); //Draw a line which connect to the previous location

            if (patrolPoints.Count > 2 && i == patrolPoints.Count - 1) //If we have more than 3 or more points
            {
                Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[0].position); //Draw a line between the last point and the 1st point
            }
        }
    }
}
