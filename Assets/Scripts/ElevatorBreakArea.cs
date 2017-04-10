using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBreakArea : MonoBehaviour {
	public bool used = false;
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Elevator" && used == false) {
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.transform.root.GetComponent<Elevator>().broken = true;
			//Timer.elevatorBrokenTimer = true;
			GameMaster.gm.SavePoint = transform;
			//Debug.Log (GameMaster.gm.SavePoint.position);
			// Destroy the rocket.
			used = true;
		}
	}
}
