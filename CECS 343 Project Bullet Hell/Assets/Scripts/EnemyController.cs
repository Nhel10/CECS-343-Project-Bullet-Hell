using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float health;

	public float projectileSpeed;
	public float fireRate;

	public GameObject projectile;
	public GameObject powerUpSpawns;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "PlayerProjectile") {
			Destroy (col.gameObject);
		}
	

	}
}
