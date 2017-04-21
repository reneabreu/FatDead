using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prog2 : MonoBehaviour {

	public float speed = 3f;

	private Rigidbody2D rigidBody2d;

    // Use this for initialization
    void Start () {
		rigidBody2d = this.gameObject.GetComponent<Rigidbody2D> ();
    }
	
	// Update is called once per frame
	void Update () {

		rigidBody2d.velocity = new Vector2 (speed * Input.GetAxis ("Horizontal"), speed * Input.GetAxis("Vertical"));

    }
}
