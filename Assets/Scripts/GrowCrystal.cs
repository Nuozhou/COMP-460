using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCrystal : MonoBehaviour {

	void OnCollisonEnter2D(Collision2D coll) {
		if (coll.transform.tag == "Player") {
			if (coll.transform.name == "Human") {
				coll.gameObject.GetComponent<Human> ().DamageHuman (20);
			}

			if (coll.transform.name == "Alien") {
				coll.gameObject.GetComponent<Alien> ().DamageAlien (20);
			}
		}
	}
}
