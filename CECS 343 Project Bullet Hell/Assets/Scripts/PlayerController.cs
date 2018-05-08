using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed = 5.0f;
	public float padding = 0.5f;
	public float projectileSpeed;
	public float fireRate;
    public float health = 5000f;
    public Slider healthSlider;     // UI to show player health

	public GameObject laser;


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

	void Fire(){
		GameObject beam = Instantiate (laser, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, projectileSpeed);

		Vector2 newPosition = new Vector2 (transform.position.x + .5f , transform.position.y);
		GameObject beam2 = Instantiate (laser, newPosition, Quaternion.identity) as GameObject;
		beam2.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, projectileSpeed);

		Vector2 newPosition2 = new Vector2 (transform.position.x - .5f , transform.position.y);
		GameObject beam3 = Instantiate (laser, newPosition2, Quaternion.identity) as GameObject;
		beam3.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, projectileSpeed);
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

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.0000001f, fireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke ("Fire");
		}

		moveShip ();

		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp (transform.position.y, yMin, yMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		transform.position = new Vector3 (transform.position.x, newY, transform.position.z);

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
}
