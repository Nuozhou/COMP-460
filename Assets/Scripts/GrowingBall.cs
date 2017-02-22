using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBall : MonoBehaviour {
	float rate = 0.001f;

	void Start() {
		GetComponent<Rigidbody2D> ().simulated = false;
	}

	void Update () {
		if (GetComponent<Rigidbody2D> ().simulated) {
			Grow ();
		}
	}

	void Grow() {
		transform.localScale = new Vector3(transform.localScale.x+rate, transform.localScale.y+rate, transform.localScale.z+rate); 
	}

	public IEnumerator DestroyAfterAwake() {
		yield return new WaitForSeconds (10f);
		//Destroy (gameObject);
	}
		
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.transform.tag == "Player") {
			Destroy (gameObject);
			if (coll.transform.name == "Human") {
				coll.transform.GetComponent<Human> ().DamageHuman (40);
			} 
			if (coll.transform.name == "Alien") {
				coll.transform.GetComponent<Alien> ().DamageAlien (40);
			}
		}
	}
}
