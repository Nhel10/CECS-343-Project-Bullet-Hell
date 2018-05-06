using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float damage = 100f; //damage caused by projectile

    // function triggers on hit
    public void Hit()
    {
        Destroy(gameObject);    // destroy the projectile that collided with enemy
    }

    public float getDamage()
    {
        return damage;  // return the amount of damage
    }
}
