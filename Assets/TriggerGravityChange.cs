using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGravityChange : MonoBehaviour {

	private bool used = false;
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player" && col.name == "Human" && !used) {
			Physics2D.gravity *= -1;
			used = true;
		}
	}
}
