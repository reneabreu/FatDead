using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour {
	
	void Awake(){

	}

	void OnTriggerEnter2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			transform.parent.GetComponent<Zombie> ().NearPlayer = true;
			other.gameObject.GetComponent<Player> ().Attacked(transform.parent.GetComponent<Zombie> ().facingRight);
			Debug.Log ("Player suffered Damage");
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if (other.gameObject.tag == "Player") {
			transform.parent.GetComponent<Zombie> ().NearPlayer = false;
			Debug.Log ("Player is running from me");
		}
	}
}
