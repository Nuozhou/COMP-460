using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
		if(col.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Damage(100);

			// Destroy the rocket.
			Destroy (gameObject);
		}
	}

}
