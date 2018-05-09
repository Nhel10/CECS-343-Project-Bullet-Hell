using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed = 5.0f;
	public float padding = 0.5f;

	public float laserProjectileSpeed;
	public float missileProjectileSpeed;
	public float bombProjectileSpeed;

	public float laserFireRate;
	public float missileFireRate;
	public float bombFireRate;

	public GameObject laser;
	public GameObject missile;
	public GameObject bomb;

	public bool upgradeLV2 = false;
	public bool upgradeLV3 = false;

	public int weaponType1 = 1;
	public int weaponType2 = 1;
	public int weaponType3 = 1;

    public float health = 5000f;
    public Slider healthSlider;     // UI to show player health


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

	GameObject determineGameObject(int type){
		GameObject returnObject = laser;
		switch(type){
			case 1:
			returnObject = laser;
			break;
			case 2:
			returnObject = missile;
			break;
			case 3:
			returnObject = bomb;
			break;
		}
		return returnObject;
	}

	float determineProjectileSpeed(int type){
		float returnProjectileSpeed = 0;
		switch(type){
			case 1:
			returnProjectileSpeed = laserProjectileSpeed;
			break;
			case 2:
			returnProjectileSpeed = missileProjectileSpeed;
			break;
			case 3:
			returnProjectileSpeed = bombProjectileSpeed;
			break;
		}
		return returnProjectileSpeed;
	}

	void FireLV1(){
		GameObject beam = Instantiate (determineGameObject(weaponType1), transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, determineProjectileSpeed(weaponType1));
	}

	void FireLV2(){

		Debug.Log("Lv2 is shooting!");
		Vector2 newPosition = new Vector2 (transform.position.x + .25f , transform.position.y);
		GameObject beam2 = Instantiate (determineGameObject(weaponType2), newPosition, Quaternion.identity) as GameObject;
		beam2.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, determineProjectileSpeed(weaponType2));

		Vector2 newPosition2 = new Vector2 (transform.position.x - .25f , transform.position.y);
		GameObject beam3 = Instantiate (determineGameObject(weaponType2), newPosition2, Quaternion.identity) as GameObject;
		beam3.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, determineProjectileSpeed(weaponType2));

	}

	void FireLV3(){

		Debug.Log("Lv3 is shooting!");
		// Right Missile Launcher
		Vector2 rightMissile = new Vector2 (transform.position.x + 0.5f, transform.position.y + 0.2f);
		GameObject beam5 = Instantiate (determineGameObject(weaponType3), rightMissile, Quaternion.identity) as GameObject;
		beam5.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, determineProjectileSpeed(weaponType3));

		// Left Missile Launcher
		Vector2 leftMissile = new Vector2 (transform.position.x - 0.5f, transform.position.y + 0.2f);
		GameObject beam6 = Instantiate (determineGameObject(weaponType3), leftMissile, Quaternion.identity) as GameObject;
		beam6.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, determineProjectileSpeed(weaponType3));

	}

	float determineFireRate(int type){
		float returnFireRate = 0;
		switch(type){
			case 1:
			returnFireRate = laserFireRate;
			break;
			case 2:
			returnFireRate = missileFireRate;
			break;
			case 3:
			returnFireRate = bombFireRate;
			break;
		}
		return returnFireRate;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log(upgradeLV2);
		Debug.Log(upgradeLV3);
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("FireLV1", 0.0000001f, determineFireRate(weaponType1));
			if(upgradeLV2 == true){
				InvokeRepeating("FireLV2", 0.0000001f,determineFireRate(weaponType2));
			}
			if(upgradeLV3 == true){
				InvokeRepeating("FireLV3", 0.0000001f, determineFireRate(weaponType3));
			}
		}

		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke ("FireLV1");
			CancelInvoke ("FireLV2");
			CancelInvoke ("FireLV3");
		}

		moveShip ();

		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp (transform.position.y, yMin, yMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		transform.position = new Vector3 (transform.position.x, newY, transform.position.z);

	}

    private void OnTriggerEnter2D(Collider2D collision){
        Projectile laser = collision.gameObject.GetComponent<Projectile>();
        if (laser)  // if what the player collides with is a projectilee
        {
            health -= laser.getDamage();    // player loses health equal to projectile damage
            healthSlider.value = health;    // update UI healthbar
            laser.Hit();    // register that a collision was made
            if (health <= 0)    // if health is reduced to zero
            {
                Destroy(gameObject);    //destroy the enemy
            }
            Debug.Log("Hit by laser");
        }
    }

		public void addLaserFireRate(){
			laserFireRate = laserFireRate + 0.01f;
		}
}
