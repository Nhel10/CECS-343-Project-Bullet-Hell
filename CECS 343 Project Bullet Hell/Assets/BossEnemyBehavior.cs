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
	
	// Update is called once per frame
	void Update () {
		if (curWaypoint < waypoints.Length)
        {
            target = waypoints[curWaypoint].position;
            moveDirection = target - transform.position;
            velocity = GetComponent<RigidBody2D>().velocity;

            if (moveDirection.magnitude < 1)
            {
                curWaypoint++;
            }
            else
            {
                velocity = moveDirection.normalized * speed;
            }
        }
	}
}
