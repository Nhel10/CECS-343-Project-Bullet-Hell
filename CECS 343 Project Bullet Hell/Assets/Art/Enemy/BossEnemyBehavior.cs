using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBehavior : MonoBehaviour {
    public float health = 1000f;            // health of enemy object


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
