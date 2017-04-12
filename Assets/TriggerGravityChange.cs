using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGravityChange : MonoBehaviour {

	private bool used = false;
	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player" && col.name == "Human" && !used) {
			if (Physics2D.gravity.y >= 0f) {
				Physics2D.gravity = new Vector2(0f, -9.8f);
			}
			used = true;
		}
	}
}
