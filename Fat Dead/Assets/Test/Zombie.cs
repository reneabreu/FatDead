using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour {

	public int life = 3;
	public GameObject player;
	public float speed = 3f;
	public bool facingRight = true;
	public GameObject Explosion;

	// Use this for initialization
	void Start () {
		
	}

	void Update(){
		if (player.transform.position.x > this.transform.position.x && !facingRight)
			Flip ();
		else if (player.transform.position.x < this.transform.position.x && facingRight)
			Flip ();
	}
	void FixedUpdate () {
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
		
	}

	public void ReceiveDamage(int damage){
		life -= damage;
		Debug.Log ("Zombie's life: " + life);

		if (life <= 0) {
			Instantiate (Explosion, this.transform.position, Quaternion.identity);
			Debug.Log ("Zombie is dead.");
			Destroy (this.gameObject);
		}
	}

	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
