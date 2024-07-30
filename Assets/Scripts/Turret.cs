using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private List<Transform> turretBarrels; //Store the data if there are multiple barrels
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float reloadDelay = 1f; //Cooldown for the shooting

    private bool canShoot = true;
    private Collider2D[] tankColliders; //To store the reference of the tank's colliders for the player
    private float currentDelay = 0f;

    private void Awake()
    {
        tankColliders = GetComponentsInParent<Collider2D>();
    }

    private void Update()
    {
        //Timer for the shooting cooldown
        if (canShoot == false)
        {
            currentDelay -= Time.deltaTime;
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
            currentDelay = reloadDelay;

            foreach (var barrel in turretBarrels)
            {
                GameObject bullet = Instantiate(bulletPrefab); //Spawn bullet
                bullet.transform.position = barrel.position;
                bullet.transform.localRotation = barrel.rotation;
                bullet.GetComponent<Bullet>().Initialize();

                foreach (var collider in tankColliders)
                {
                    Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), collider);
                }
            }
        }
    }
}
