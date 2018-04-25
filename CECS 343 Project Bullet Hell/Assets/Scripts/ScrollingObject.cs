using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour {
	private Rigidbody2D rigidBody;
	private float m_speed = -0.5f; 
	[SerializeField] private bool m_stopScroling;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.velocity = new Vector2(0,m_speed);
	}

	// Update is called once per frame
	void Update () {

		if (m_stopScroling)
			rigidBody.velocity = Vector2.zero;
		else
			rigidBody.velocity = new Vector2(0, m_speed);
	}
}
