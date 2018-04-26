using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5.0f;
	public float padding = 0.5f;
	public float projectileSpeed;
	public float fireRate;

	public GameObject projectile;

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
		GameObject beam = Instantiate (projectile, transform.position, Quaternion.identity) as GameObject;
		beam.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, projectileSpeed, 0);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			InvokeRepeating ("Fire", 0.0000001f, fireRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke ("Fire");
		}


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


		float newX = Mathf.Clamp (transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp (transform.position.y, yMin, yMax);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
		transform.position = new Vector3 (transform.position.x, newY, transform.position.z);

	}
}
