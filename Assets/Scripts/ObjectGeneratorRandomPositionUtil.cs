using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGeneratorRandomPositionUtil : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private float raidus = 0.2f;  

    public void CreateObject()
    {
        Vector2 position = GetRandomPosition();
        GameObject impactObject = GetObject();
        impactObject.transform.position = position;
        impactObject.transform.rotation = Random2DRotation();
    }

    protected GameObject GetObject()
    {
        return Instantiate(objectPrefab);
    }

    protected Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * raidus + (Vector2)transform.position;
    }

    protected Quaternion Random2DRotation()
    {
        return Quaternion.Euler(0, 0, Random.Range(0,360));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, raidus);
    }
}
