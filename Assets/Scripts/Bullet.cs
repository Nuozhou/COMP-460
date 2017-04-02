using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public int damage = 50;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject, 2);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Enemy") {
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy> ().Damage (damage);

			// Destroy the rocket.
			Destroy (gameObject);
		} else if (col.tag == "Turret") {
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<TurretAI> ().Damage (damage);

			// Destroy the rocket.
			Destroy (gameObject);
		} else if (col.tag == "Boss") {
			col.gameObject.GetComponent<BossAI> ().Damage (damage);
			Destroy (gameObject);
		} 
	}


}
