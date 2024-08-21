using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMarkSpawner : MonoBehaviour
{
    [SerializeField] private float trackDistance = 0.2f; //Distance between the track mark
    [SerializeField] private GameObject trackPrefab;
    [SerializeField] private int objectPoolSize = 50;

    private Vector2 lastPosition; //the last position we have place for the mark
    private ObjectPool objectPool;
    private void Awake()
    {
        objectPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        lastPosition = transform.position; //set the lastPosition to the current position of the tank
        objectPool.Initialize(trackPrefab, objectPoolSize); //initialize the object pool
    }

    private void Update()
    {
        var distanceDriven = Vector2.Distance(transform.position, lastPosition);
        if (distanceDriven >= trackDistance)  //if the distanceDriven by tank is greater and equal to the in-between distance for the mark
        {
            lastPosition = transform.position; //set the last position to new tank location
            var tracks = objectPool.CreateObject(); 
            tracks.transform.position = transform.position;
            tracks.transform.rotation = transform.rotation;
        }
    }
}
