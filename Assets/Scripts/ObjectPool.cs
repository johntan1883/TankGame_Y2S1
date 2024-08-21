using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Transform spawnedObjectsParent; //An object to hold all the spawned objects under one parent in hierachy
    public bool alwaysDestroy = false;

    [SerializeField] protected GameObject objectToPool; //The type of object to be pooled
    [SerializeField] protected int poolSize = 10;

    protected Queue<GameObject> objectPool;

    private void Awake()
    {
        objectPool = new Queue<GameObject>(); //initial the queue
    }

    public void Initialize(GameObject objectToPool, int poolSize = 10) //This method allow us to set the object to be pooled and pool size, it can be called from another script
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();

        GameObject spawnedObject = null;

        if (objectPool.Count < poolSize) //if the pool is not full, it creates a new object, sets its name and parent.
        {
            spawnedObject = Instantiate(objectToPool, transform.position, Quaternion.identity);
            spawnedObject.name = transform.name + "_" + objectToPool.name + "-" + objectPool.Count;
            spawnedObject.transform.SetParent(spawnedObjectsParent);
            spawnedObject.AddComponent<DestroyIfDisable>();
        }
        else //if the pool is full, it reueses an object from the pool by dequeuing it, resetting its position and rotation and activating it.
        {
            spawnedObject = objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }

        objectPool.Enqueue(spawnedObject); //Adds the created or reused object back to the pool
        return spawnedObject;
    }

    private void CreateObjectParentIfNeeded() //This method ensures that there is a parent object to hold all spawned objects, organizing them in hierachy
    {
        if (spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + objectToPool.name;
            var parentObject = GameObject.Find(name);
            if (parentObject != null)
            {
                spawnedObjectsParent = parentObject.transform;
            }
            else
            {
                spawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }

    private void OnDestroy() //this will be call just before we destroy the object
    {
        foreach(var item in objectPool)
        {
            if (item == null)
            {
                continue;
            }
            else if (item.activeSelf == false || alwaysDestroy) //if the object is disabled, we destroy the disabled bullet
            {
                Destroy(item);
            }
            else //if the object is still enable, we set the SelfDestructionEnabled in the type <DestroyIfDisable> to true
            {
                item.GetComponent<DestroyIfDisable>().SelfDestructionEnabled = true; 
            }
        }
    }
}
