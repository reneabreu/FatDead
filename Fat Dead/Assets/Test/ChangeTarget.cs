using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTarget : MonoBehaviour {

	public GameObject[] zombies;

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Player") {

			Debug.Log ("Player detected");
			foreach (GameObject zombie in zombies) {
				zombie.GetComponent<Zombie> ().target = other.gameObject;
			}

			Destroy (this.gameObject);
		}
	}
}
