using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){

		if (other.gameObject.tag == "Zombie") {
			other.gameObject.GetComponent<Zombie> ().ReceiveDamage(1);
			
			Destroy (this.gameObject);
		} else {
			Destroy (this.gameObject);
		}	
	}
}
