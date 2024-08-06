using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float maxDistance = 10f;
    //[SerializeField] private ParticleSystem hitEffect;

    private Vector2 startPosition;
    private float conquaredDistance = 0f;
    private Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        conquaredDistance = Vector2.Distance(transform.position, startPosition); //Calculate the distance between bullet and the startPosition

        if (conquaredDistance >= maxDistance) //When the travel distance has exceeded maxDistance, disable the bullet
        {
            DisableObject();
        }
    }

    private void DisableObject()
    {
        rb2d.velocity = Vector2.zero; //Reset the speed for bullet
        gameObject.SetActive(false); //Disable the game object
    }

    public void Initialize()
    {
        startPosition = transform.position; //Starting travel position for bullet
        rb2d.velocity = transform.up * speed; //Travel speed for bullet
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided " + collision.name);

        var damageable = collision.GetComponent<Damageable>(); //To check if the collision is damageable
        if (damageable != null)
        {
            damageable.Hit(damage);
        }

        //SpawnHitEffect();
        DisableObject();
    }

    //private void SpawnHitEffect()
    //{
    //    if (hitEffect != null)
    //    {
    //        ParticleSystem effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    //        effect.Play();
    //        Destroy(effect.gameObject, effect.main.duration); // Destroy the particle system after it finishes
    //    }
    //}
}
