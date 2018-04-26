using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float width = 12f; // Width of the formation
    public float height = 4f; // Height of the formation
    public float speed = 3f; // Moving speed of the formation
    public float spawnDelay = 0.5f; // Timing delay to have enemies spawn individually

    private bool movingRight = true; // Bool to track if the formation should be moving
                                     // to the right
    private float xmax; // Maximum x value (Right edge of play area)
    private float xmin; // Minimum x value (Left edge of play area)

    // Use this for initialization
    void Start () {
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftBoundary = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera));
        Vector3 rightBoundary = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera));

        // Set xmax and xmin to be the boundaries of the play area
        xmax = rightBoundary.x;
        xmin = leftBoundary.x;

        SpawnUntilFull();
	}

    // Spawn all enemies at once
    void SpawnEnemies() {
        foreach (Transform child in transform) {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    // Spawn by iterating through the available positions
    void SpawnUntilFull() {
        Transform nextPosition = NextFreePosition();
        if (nextPosition) {
            GameObject enemy = Instantiate(enemyPrefab, nextPosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = nextPosition;
        }
        if (NextFreePosition()) {
            Invoke("SpawnUntilFull", spawnDelay);
        }
    }

    // Iterate through available positions and find any empty ones, return them
    Transform NextFreePosition() {
        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount == 0) {
                return childPositionGameObject;
            }
        }
        return null;
    }

    // Create yellow frame to track formation in scene view
    public void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {
        // If movingRight is true, move the formation to the right
		if (movingRight) {
            transform.position += Vector3.right * speed * Time.deltaTime;
        } else {
            // Else move the formation to the left
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        // Find the edges of the formation
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        float leftEdgeOfFormation = transform.position.x - (0.5f * width);

        // If the right edge of the formation moves past right edge
        // Assign movingRight to false to change direction
        if (rightEdgeOfFormation > xmax) {
            movingRight = false;
        } else if (leftEdgeOfFormation < xmin) {
            // Else if the left side of the formation moves past the left edge
            // Assign movingRight to true to change direction
            movingRight = true;
        }

        // If all enemies are dead, spawn the next formation
        if (AllEnemiesDead()) {
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }
	}

    // Check to see if all enemies are dead
    bool AllEnemiesDead() {
        foreach (Transform childPositionGameObject in transform) {
            if (childPositionGameObject.childCount > 0) {
                return false;
            }
        }
        return true;
    }
}