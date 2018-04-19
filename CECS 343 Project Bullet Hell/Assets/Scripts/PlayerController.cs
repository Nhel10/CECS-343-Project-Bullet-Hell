using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5.0f;

	float xMin = -5;
	float xMax = 5;

	void Start(){
		//Camera.main.ViewportToWorldPoint(new Vector3(
	}

	// Update is called once per frame
	void Update () {
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
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);

	}
}
