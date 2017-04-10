using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAndDestroy : MonoBehaviour {

	void Start() {
		Destroy (gameObject, 5f);
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.transform.tag == "Player") {
			if (coll.transform.name == "Human") {
				coll.transform.GetComponent<Human> ().DamageHuman (50);
			} else if (coll.transform.name == "Alien") {
				coll.transform.GetComponent<Alien> ().DamageAlien (50);
			}
			Destroy (gameObject);
		} else {
			Destroy (gameObject);
		}
	}
}
