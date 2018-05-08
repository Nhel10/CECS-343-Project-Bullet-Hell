using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5.0f;
	public float padding = 0.5f;

	public float laserProjectileSpeed;
	public float missileProjectileSpeed;
	public float bombProjectileSpeed;

	public float laserFireRate;
	public float missileFireRate;
	public float bombFireRate;

    public float health = 5000f;

	public int weaponType1 = 1; // Depending on the number will determine what type of weapon the player will wield 
	public int weaponType2 = 1; 
	public int weaponType3 = 1;

	public bool cannonLV2 = false;
	public bool cannonLV3 = false;

	public GameObject laser;
	public GameObject missile;
	public GameObject bomb;

	float xMin;
	float xMax;

	float yMin;
	float yMax;

	void Start(){
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0,0,distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xMin = leftmost.x + padding;
		xMax = rightmost.x - padding;

		Vector3 upMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, distance));
		Vector3 downMost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		yMin = downMost.y + padding;
		yMax = upMost.y - padding;
	}

	void FireLV1(){
		GameObject projectile;
		float projectileSpeed;
		switch (weaponType1) {
		case 1:
			projectile = laser;
			projectileSpeed = laserProjectileSpeed;
			break;
		case 2:
			projectile = missile;
			projectileSpeed = missileProjectileSpeed;
			break;
		case 3:
			projectile = bomb;
			projectileSpeed = bombProjectileSpeed;
			break;
		}

		// Center Laser 
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, projectileSpeed);
	
	}

	void FireLV2(){

		GameObject projectile;
		float projectileSpeed;
		switch (weaponType2) {
		case 1:
			projectile = laser;
			projectileSpeed = laserProjectileSpeed;
			break;
		case 2:
			projectile = missile;
			projectileSpeed = missileProjectileSpeed;
			break;
		case 3:
			projectile = bomb;
			projectileSpeed = bombProjectileSpeed;
			break;
		}

		// Center Laser 
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, projectileSpeed);

	}

	void FireLV3(){
		
		GameObject projectile;
		float projectileSpeed;
		switch (weaponType3) {
		case 1:
			projectile = laser;
			projectileSpeed = laserProjectileSpeed;
			break;
		case 2:
			projectile = missile;
			projectileSpeed = missileProjectileSpeed;
			break;
		case 3:
			projectile = bomb;
			projectileSpeed = bombProjectileSpeed;
			break;
		}

		// Center Laser 
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, projectileSpeed);

	}

	void moveShip(){
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.left * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			//transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.UpArrow)) {
			//transform.position += new Vector3 (0, speed * Time.deltaTime, 0);
			transform.position += Vector3.up * speed * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
			//transform.position += new Vector3 (0, -speed * Time.deltaTime, 0);
			transform.position += Vector3.down * speed * Time.deltaTime;
		} 

		if (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += new Vector3 (-speed * .5f * Time.deltaTime, -speed * .5f * Time.deltaTime, 0);
		} else if (Input.GetKey (KeyCode.DownArrow) && Input.GetKey (KeyCode.RightArrow)) {
			transform.position += new Vector3 (speed * .5f * Time.deltaTime, -speed* .5f * Time.deltaTime, 0);
		} else if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.LeftArrow)) {
			transform.position += new Vector3 (-speed* .5f * Time.deltaTime, speed* .5f * Time.deltaTime, 0);
		} else if (Input.GetKey (KeyCode.UpArrow) && Input.GetKey (KeyCode.RightArrow)) {
			transform.position += new Vector3 (speed* .5f * Time.deltaTime, speed* .5f * Time.deltaTime, 0);
		}
	}

	void fireWeapon(){

		float fireRate = 0;


		if (Input.GetKeyDown (KeyCode.Space)) {
			
			switch (weaponType1) {
			case 1:
				fireRate = laserFireRate;
				break;
			case 2:
				fireRate = missileFireRate;
				break;
			case 3:
				fireRate = bombFireRate;
				break;
			}

			InvokeRepeating ("FireLV1", 0.0000001f, fireRate);

			if (cannonLV2 == true) {
				
				switch (weaponType2) {
				case 1:
					fireRate = laserFireRate;
					break;
				case 2:
					fireRate = missileFireRate;
					break;
				case 3:
					fireRate = bombFireRate;
					break;
				}

				InvokeRepeating ("FireLV2", 0.0000001f, fireRate);
			}
			if (cannonLV3 == true) {

				switch (weaponType3) {
				case 1:
					fireRate = laserFireRate;
					break;
				case 2:
					fireRate = missileFireRate;
					break;
				case 3:
					fireRate = bombFireRate;
					break;
				}

				InvokeRepeating ("FireLV3", 0.0000001f, fireRate);
			}
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke ("FireLV1");
			CancelInvoke ("FireLV2");
			CancelInvoke ("FireLV3");
		}
	}

	// Update is called once per frame
	void Update () {

		fireWeapon ();
		moveShip ();

		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp (transform.position.y, yMin, yMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		transform.position = new Vector3 (transform.position.x, newY, transform.position.z);

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
