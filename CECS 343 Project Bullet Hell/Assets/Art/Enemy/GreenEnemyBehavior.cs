﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenEnemyBehavior : MonoBehaviour
{

    public GameObject projectile;
    public float enemyMissileSpeed = 10f;
    public float health = 1000f;    // health of enemy object


    private void Start()
    {
        InvokeRepeating("fire", 0.0f, 1.5f);
    }


    private void fire()
    {
        GameObject enemyMissile1 = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        enemyMissile1.GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, -enemyMissileSpeed);

        GameObject enemyMissile2 = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        enemyMissile2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyMissileSpeed);

        GameObject enemyMissile3 = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        enemyMissile3.GetComponent<Rigidbody2D>().velocity = new Vector2(0.5f, -enemyMissileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile laser = collision.gameObject.GetComponent<Projectile>();
        if (laser)
        {
            health -= laser.getDamage();
            laser.Hit();    // register that a collision was made
            if (health <= 0)    // if health is reduced to zero
            {
                Destroy(gameObject);    //destroy the enemy  
            }
            Debug.Log("Hit by laser");
        }
    }
}