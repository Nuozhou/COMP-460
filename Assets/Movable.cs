using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : MonoBehaviour {
	private Vector3 originalPosition;
	public float constrainRight = 5f;
	public float constrainLeft = 5f;
	public float constrainUp = 5f;
	public float constrainDown = 5f;

	void Awake() {
		originalPosition = gameObject.transform.position;
	}

	void FixedUpdate() {

		float newX = transform.position.x;
		float newY = transform.position.y;

		if (transform.position.x > originalPosition.x + constrainRight) {
			newX = originalPosition.x + constrainRight;
		}
		if (transform.position.x < originalPosition.x - constrainLeft) {
			newX = originalPosition.x - constrainLeft;
		}
		if (transform.position.y > originalPosition.y + constrainUp) {
			newY = originalPosition.y + constrainUp;
		} 
		if (transform.position.y < originalPosition.y - constrainDown) {
			newY = originalPosition.y - constrainDown;
		} 

		transform.position = new Vector3 (newX, newY, transform.position.z);
	}
}
