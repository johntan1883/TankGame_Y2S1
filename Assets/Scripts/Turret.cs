using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ObjectPool))] //this attribute ensure that the component for ObjectPool exists
public class Turret : MonoBehaviour
{
    public UnityEvent OnShoot, OnCantShoot;
    public UnityEvent<float> OnReloading; 

    [SerializeField] private List<Transform> turretBarrels; //Store the data if there are multiple barrels
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private TurretData turretData;

    private bool canShoot = true;
    private Collider2D[] tankColliders; //To store the reference of the tank's colliders for the player
    private float currentDelay = 0f;

    private ObjectPool bulletPool;
    [SerializeField] private int bulletPoolCount = 10;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
        bulletPool = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        bulletPool.Initialize(turretData.bulletPrefab, bulletPoolCount);
        OnReloading?.Invoke(currentDelay);
    }

    private void Update()
    {
        //Timer for the shooting cooldown
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
            OnReloading?.Invoke(currentDelay / turretData.reloadDelay);
            if (currentDelay <= 0)
            {
                canShoot = true;
            }
        }
    }

    public void Shoot()
    {
        if (canShoot)
        {
            canShoot = false;
            currentDelay = turretData.reloadDelay;

            foreach (var barrel in turretBarrels)
            {
                //GameObject bullet = Instantiate(bulletPrefab); //Spawn bullet
                GameObject bullet = bulletPool.CreateObject();//Spawning the bullet with the pool
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize(turretData.bulletData);

                foreach (var collider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }

            OnShoot?.Invoke();
            OnReloading?.Invoke(currentDelay);
        }
        else
        {
            OnCantShoot?.Invoke();
        }
        
    }
}
