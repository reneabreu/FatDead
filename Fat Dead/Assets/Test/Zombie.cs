using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	public int life = 3;
	public GameObject target;
	public float speed = 3f;
	public bool facingRight = true;
	public GameObject Explosion;

	[HideInInspector]public bool NearPlayer = false;

	private Animator animator;
	private Rigidbody2D rigidBody2D;

	void Awake(){
		animator = this.gameObject.GetComponent<Animator> ();
		rigidBody2D = this.gameObject.GetComponent<Rigidbody2D> ();
	}

	void Update(){
		if (target.transform.position.x > this.transform.position.x && !facingRight)
			Flip ();
		else if (target.transform.position.x < this.transform.position.x && facingRight)
			Flip ();
	}
	void FixedUpdate () {
		if (!NearPlayer) {
			Walk ();
		}
	}

	public void ReceiveDamage(int damage, float bulletPos){
		life -= damage;
		Debug.Log ("Zombie's life: " + life);

		if (life <= 0) {
			Instantiate (Explosion, this.transform.position, Quaternion.identity);
			Debug.Log ("Zombie is dead.");
			Destroy (this.gameObject);
		} else {
			StartCoroutine (SufferDamage (bulletPos));
		}
	}

	void Walk(){
		animator.SetBool ("Walking", true);
		transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed*Time.deltaTime);
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public IEnumerator SufferDamage(float bulletPos){

		transform.GetComponent<SpriteRenderer> ().color = Color.red;
		if(bulletPos < this.transform.position.x)
			rigidBody2D.AddForce(10f * transform.right, ForceMode2D.Impulse);
		else
			rigidBody2D.AddForce(-(10f * transform.right), ForceMode2D.Impulse);
		yield return new WaitForSeconds (0.3f);
		transform.GetComponent<SpriteRenderer> ().color = Color.white;
	}
}
