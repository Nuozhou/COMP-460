using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawner : MonoBehaviour {

	public Transform spawner;
	public Transform spawnLocation;

	public bool isTriggered = false;

	void OnTriggerEnter2D(Collider2D col) {
		if (!isTriggered) {
			if (col.tag == "Player") {
				Transform boss = Instantiate (spawner, spawnLocation.position, Quaternion.identity);
				isTriggered = true;
			}
		} 
	}
}
