using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public float projectileSpeed = 10f;
	public GameObject projectile;

	private Player player;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot ();
		}
		
	}

	void Shoot(){
		GameObject SpawnedObject = Instantiate(projectile, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y), Quaternion.identity) as GameObject;
		if(player.facingRight)
			SpawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (projectileSpeed, 0);
		else
			SpawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-projectileSpeed, 0);
	}
}
