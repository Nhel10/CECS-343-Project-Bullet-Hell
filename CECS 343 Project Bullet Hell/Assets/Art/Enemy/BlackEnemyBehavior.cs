using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackEnemyBehavior : MonoBehaviour
{
    public GameObject projectile;           // projectile that the enemy will fire
    public float enemyMissileSpeed = 10f;   // speed that enemy projectiles travel at
    public float health = 1000f;            // health of enemy object



    private void Start()
    {
        InvokeRepeating("fire", 0.0f, 0.7f);    // repeatedly call fire function
    }


    private void fire()
    {
        //create missile slots to fire two enemy projectiles
        Vector2 missileSlot1 = new Vector2(transform.position.x + .2f, transform.position.y);
        Vector2 missileSlot2 = new Vector2(transform.position.x - .2f, transform.position.y);

        //fire first enemy projectile
        GameObject enemyMissile1 = Instantiate(projectile, missileSlot1, Quaternion.identity) as GameObject;
        enemyMissile1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyMissileSpeed);

        //fire second enemy projectile
        GameObject enemyMissile2 = Instantiate(projectile, missileSlot2, Quaternion.identity) as GameObject;
        enemyMissile2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyMissileSpeed);

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
                Destroy(gameObject);    // destroy the enemy  
            }
            Debug.Log("Hit by laser");
        }
    }
}