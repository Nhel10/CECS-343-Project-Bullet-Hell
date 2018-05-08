using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour {

    public GameObject projectile;       // projectile to be fired
    public float shotDelay = 0.1f;      // delay between projectiles
    public float projectileSpeed = 5f;  // speed that the projectile travels at

    private void Start()
    {
        Invoke("shoot", shotDelay);
    }

    // create projectile
    void shoot()
    {
        GameObject bossShot = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        bossShot.GetComponent<Rigidbody2D>().velocity = (Vector2)transform.TransformDirection(Vector3.down) * projectileSpeed;
        Invoke("shoot", shotDelay);
    }
}
