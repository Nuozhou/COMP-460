using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGrowBall : MonoBehaviour {

	public Transform growBall;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			growBall.GetComponent<Rigidbody2D> ().simulated = true;
		}
	}
}
