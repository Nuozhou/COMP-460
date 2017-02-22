using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour {

	public Transform spawner;

	public bool isTriggered = false;

	void OnTriggerEnter2D(Collider2D col) {
		if (!isTriggered) {
			if (col.tag == "Player") {
				Transform boss = Instantiate (spawner, new Vector3 (transform.position.x + 10f, transform.position.y + 5f, transform.position.z), Quaternion.identity);
				isTriggered = true;
			}
		} 
	}
}
