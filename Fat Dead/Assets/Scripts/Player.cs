using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	public float speed = 5f;
	public float jumpForce = 5f;
	public Transform groundCheck;


	private Rigidbody2D rigidBody2d;
	private bool grounded = false;

	// Use this for initialization
	void Start () {
		rigidBody2d = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
			Jump ();
		}
		
	}

	void FixedUpdate() {
		Move ();
	}

	void Move(){
		
		/*if (Input.GetKeyDown(KeyCode.UpArrow)) {
			rigidBody2d.velocity += new Vector2 (rigidBody2d.velocity.x, 5f);
		}

		float h = Input.GetAxis("Horizontal");

		Vector2 moveVel = rigidBody2d.velocity;
		moveVel.x = h * speed;
		rigidBody2d.velocity = moveVel;

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();*/

		if (Input.GetKey (KeyCode.RightArrow)) {
			if (!facingRight)
				Flip ();
			
			rigidBody2d.velocity = new Vector2 (speed, rigidBody2d.velocity.y);
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			rigidBody2d.velocity = new Vector2 (0, rigidBody2d.velocity.y);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (facingRight)
				Flip ();
			
			rigidBody2d.velocity = new Vector2 (-speed, rigidBody2d.velocity.y);
		} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			rigidBody2d.velocity = new Vector2 (0, rigidBody2d.velocity.y);
		}

		//Debug.Log (rigidBody2d.velocity);
	}

	void Jump(){
		rigidBody2d.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
	}



	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
