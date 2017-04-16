using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHelp : MonoBehaviour {

	public float minTime;
	public float maxTime;
	public float jumpForce;

	public Texture2D happy;

	// Use this for initialization
	void Start () {
		StartCoroutine (AskForHelp (Random.Range (minTime, maxTime)));
		StartCoroutine (Flip (Random.Range (minTime, maxTime)));
	}

	IEnumerator AskForHelp(float time){
		this.gameObject.GetComponent<Rigidbody2D> ().AddForce(jumpForce * transform.up, ForceMode2D.Impulse);
		yield return new WaitForSeconds (time);

		time = Random.Range (minTime, maxTime);
		StartCoroutine (AskForHelp (time));

	}

	IEnumerator Flip(float time){
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

		yield return new WaitForSeconds (time);

		time = Random.Range (minTime, maxTime);
		StartCoroutine (Flip (time));
	}
}
