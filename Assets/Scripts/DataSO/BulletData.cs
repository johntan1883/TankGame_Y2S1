using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Data/BulletData")]
public class BulletData : ScriptableObject
{
    public float speed = 100f;
    public int damage = 5;
    public float maxDistance = 10f;
}
