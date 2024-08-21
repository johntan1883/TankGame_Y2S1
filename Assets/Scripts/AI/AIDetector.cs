using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDetector : MonoBehaviour
{
    [Range(1, 15)]
    [SerializeField] private float viewRadius = 11; //Radius for the detection
    [SerializeField] private float detectionCheckDelay = 0.1f; //Value to decrease or increase the raycast time interval for detection
    [SerializeField] private LayerMask playerLayerMask; 
    [SerializeField] private LayerMask visibilityLayer; //This layermask allows us to set the layer of the obstacles that can block visibility for the ai
    [SerializeField] private Transform target = null;

    [field : SerializeField] public bool TargetVisible { get; private set; }
    public Transform Target 
    { 
        get => target;
        set
        {
            target = value;
            TargetVisible = false;
        }
    }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine()); //Initial the detection
    }

    private void Update()
    {
        if (Target != null)
        {
            TargetVisible = CheckTargetVisible();
        }
    }

    private bool CheckTargetVisible()
    {
        var result = Physics2D.Raycast(transform.position, Target.position - transform.position, viewRadius, visibilityLayer);

        if (result.collider != null)
        {
            return (playerLayerMask & (1 << result.collider.gameObject.layer)) != 0; //This creates a bitmask for the layer of the object hit by the ray. 
        }                                                                            //'playerLayerMask &...' checks if this bitmask is part of the 'playerLayerMask'. if it is, it means the ray hit something on the player layer, and the player is visible to the ai.    
        return false;
    }

    private void DetectTarget()
    {
        if (Target == null)
        {
            CheckIfPlayerInRange();
        }
        else if (Target != null) //if the player or target is in range
        {
            DetectIfOutOfRange(); //we check again if the player or target is still in range
        }
    }

    private void DetectIfOutOfRange()
    {
        if (Target == null || Target.gameObject.activeSelf == false || Vector2.Distance(transform.position, Target.position) > viewRadius + 1)
        {
            Target = null;
        }
    }

    private void CheckIfPlayerInRange()
    {
        Collider2D collision = Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask); //Using the physic2d overlap function to detection the player's collider
        if (collision != null)
        {
            Target = collision.transform;
        }
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionCheckDelay);
        DetectTarget();
        StartCoroutine(DetectionCoroutine()); //Rerun the coroutine
    }

    private void OnDrawGizmos() //Visualize the viewRaidus for the ai
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
