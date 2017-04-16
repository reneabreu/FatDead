using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	public float projectileSpeed = 10f;
	public GameObject projectile;

	public AudioClip audioClip;

	private Player player;
	private Animator animator;

	private AudioSource audioSource;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		animator = player.GetComponent<Animator> ();
		audioSource = this.gameObject.GetComponent<AudioSource> ();
	}

	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) {
			animator.SetBool ("Atirando", true);
			Shoot ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			animator.SetBool ("Atirando", false);
		}
		
	}

	void Shoot(){

		audioSource.PlayOneShot (audioClip);
		GameObject SpawnedObject = Instantiate(projectile, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y), Quaternion.identity) as GameObject;

		if(player.facingRight)
			SpawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (projectileSpeed, 0);
		else
			SpawnedObject.GetComponent<Rigidbody2D>().velocity = new Vector2 (-projectileSpeed, 0);


		animator.SetBool ("Atirando", false);
	}
}
