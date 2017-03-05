using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	private Transform target;
	private Vector3 lastPosition;

	void Start() {
		target = null;
		lastPosition = transform.position;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.tag == "Player") {
			if (coll.collider.name == "Human") {
				target = coll.collider.transform;
			}
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		target = null;
	}

	void LateUpdate(){
		if (target != null) {
			target.transform.position = transform.position - lastPosition + target.transform.position;
		}
		lastPosition = transform.position;
	}


}
