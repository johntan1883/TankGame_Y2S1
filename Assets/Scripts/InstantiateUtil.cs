using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateUtil : MonoBehaviour
{
    [SerializeField] private GameObject objectToInstantiate;

    public void InstantiateObject()
    {
        Instantiate(objectToInstantiate);
    }
}
