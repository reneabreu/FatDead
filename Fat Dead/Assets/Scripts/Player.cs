using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	public float speed = 5f;
	public float jumpForce = 5f;

	public Transform groundCheck;

	public Text lives;

	public GameObject HUD;
	public GameObject Demo;

	public AudioClip claps;
	public AudioClip jump;

	private Rigidbody2D rigidBody2d;
	private bool grounded = false;
	private Animator animator;

	private AudioSource audioSource;

	private int Vida = 3;

	// Use this for initialization
	void Start () {
		rigidBody2d = this.gameObject.GetComponent<Rigidbody2D>();
		animator = this.gameObject.GetComponent<Animator> ();
		audioSource = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		lives.text = "x " + Vida;

		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (grounded)
			animator.SetBool ("Jumping", false);

		if (Input.GetKeyDown (KeyCode.UpArrow) && grounded) {
			Jump ();
		}

		if (Vida <= 0) {
			ReloadScene ();
		}

		Move ();
	}

	void FixedUpdate() {
		//Move ();
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

			if (!audioSource.isPlaying && grounded) {
				audioSource.Play ();
			}

			animator.SetBool ("Walking", true);
			rigidBody2d.velocity = new Vector2 (speed, rigidBody2d.velocity.y);
		} else if (Input.GetKeyUp (KeyCode.RightArrow)) {
			animator.SetBool ("Walking", false);
			rigidBody2d.velocity = new Vector2 (0, rigidBody2d.velocity.y);
			audioSource.Stop ();
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			if (facingRight)
				Flip ();

			if (!audioSource.isPlaying && grounded) {
				audioSource.Play ();
			}

			animator.SetBool ("Walking", true);
			rigidBody2d.velocity = new Vector2 (-speed, rigidBody2d.velocity.y);
		} else if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			animator.SetBool ("Walking", false);
			rigidBody2d.velocity = new Vector2 (0, rigidBody2d.velocity.y);
			audioSource.Stop ();

		}

		//Debug.Log (rigidBody2d.velocity);
	}

	void Jump(){
		audioSource.PlayOneShot (jump);
		rigidBody2d.AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
		animator.SetBool ("Jumping", true);
	}

	void Flip(){
		
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Attacked(bool zombieFacingRight){
		StartCoroutine (SufferDamage ());

		if(zombieFacingRight)
			rigidBody2d.AddForce(7f * transform.right, ForceMode2D.Impulse);
		else
			rigidBody2d.AddForce(-(7f * transform.right), ForceMode2D.Impulse);
	}
	public IEnumerator SufferDamage(){
		

		Vida--;

		transform.GetComponent<SpriteRenderer> ().color = Color.red;
		yield return new WaitForSeconds (0.3f);
		transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}

	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.tag == "Zombie") {
			StartCoroutine (SufferDamage ());
		}

		if (other.gameObject.tag == "SaveMe") {

			rigidBody2d.constraints = RigidbodyConstraints2D.FreezeAll;
			this.gameObject.GetComponentInChildren<Gun> ().enabled = false;
			HUD.gameObject.SetActive (false);
			Demo.gameObject.SetActive (true);
			audioSource.PlayOneShot (claps);
			this.gameObject.GetComponent<Player> ().enabled = false;
			Time.timeScale = 0;
		}
	}

	public void ReloadScene(){
		Time.timeScale = 1;
		SceneManager.LoadScene (0);
	}
}
