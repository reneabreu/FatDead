using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	public float time = 1f;

	// Use this for initialization
	void Start () {
		StartCoroutine (SelfDestroy());
	}

	IEnumerator SelfDestroy(){
		yield return new WaitForSeconds(time);

		Destroy (this.gameObject);
	}


}
