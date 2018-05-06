using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyBehavior : MonoBehaviour {
    public Transform[] waypoints;   // waypoints for the boss to travel to
    public float speed;             // speed at which the boss travels
    public int curWaypoint;         // current waypoint in the array
    public bool patrol = true;      // Determines if the boss should be moving or not
    public Vector3 target;          // target direction
    public Vector3 moveDirection;   // finds the direction that the boss needs to go to
    public Vector3 velocity;        // controls the direction in which the boss moves

    public GameObject projectile;           // projectile that the enemy will fire
    public float enemyMissileSpeed = 10f;   // speed that enemy projectiles travel at
    public float health = 100000f;          // health of enemy object

    // Update is called once per frame
    void Update () {
		if (curWaypoint < waypoints.Length) // we have not iterated through all of the waypoints yet
        {
            target = waypoints[curWaypoint].position;           // find next target location
            moveDirection = target - transform.position;        // calculate the direction to change
            velocity = GetComponent<Rigidbody2D>().velocity;    // set velocity in new direction

            if (moveDirection.magnitude < 1)    // if we have reached the current waypoint
            {
                curWaypoint++;  // iterate to next waypoints in the array
            }
            else
            {
                velocity = moveDirection.normalized * speed;    // move towards current waypoint at set speed
            }
        }
        else   // we have iterated through all of the waypoints   
        {
            if (patrol) // if patrol is true, the object will continue to loop through waypoints
            {
                curWaypoint = 0;    // reset to beginning of array to loop
            }
            else    // if patrol is false
            {
                velocity = Vector3.zero;    //set object to stop moving
            }
        }
        GetComponent<Rigidbody2D>().velocity = velocity;    //get new velocity to continue moving
	}

    // function to fire projectiles
    private void fire()
    {
        GameObject enemyMissile1 = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        enemyMissile1.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, -enemyMissileSpeed);

        GameObject enemyMissile2 = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        enemyMissile2.GetComponent<Rigidbody2D>().velocity = new Vector2(3f, -enemyMissileSpeed);
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
