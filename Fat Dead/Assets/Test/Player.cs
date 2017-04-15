using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	public float speed = 5f;
	public float projectileSpeed = 10f;
	public GameObject projectile;
	public Transform ShootingPoint;
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

		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot ();
		}

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
		}*/

		float h = Input.GetAxis("Horizontal");

		Vector2 moveVel = rigidBody2d.velocity;
		moveVel.x = h * speed;
		rigidBody2d.velocity = moveVel;

		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();

		Debug.Log (rigidBody2d.velocity);
	}

	void Jump(){
		rigidBody2d.velocity += 5f * Vector2.up;
	}

	void Shoot(){
		GameObject SpawnedObject = Instantiate(projectile, new Vector3(ShootingPoint.position.x, ShootingPoint.transform.position.y), Quaternion.identity) as GameObject;
		if(facingRight)
			SpawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (projectileSpeed, 0);
		else
			SpawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-projectileSpeed, 0);
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
