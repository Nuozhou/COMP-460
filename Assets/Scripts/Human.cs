using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {

	public int health = 100;

	public int fallBoundary = -100;

	public void DamageHuman(int damage) {
		health -= damage;
		if (health <= 0) {
			GameMaster.KillHuman (this);
		}
	}

	void Update() {
		if (transform.position.y <= fallBoundary)
			DamageHuman(1000);
	}
}
