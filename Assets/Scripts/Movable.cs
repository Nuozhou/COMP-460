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
		Debug.Log ("start position: " + originalPosition);
	}

	void FixedUpdate() {

		float newX = transform.position.x;
		float newY = transform.position.y;

		if (transform.position.x > originalPosition.x + constrainRight) {
			Debug.Log ("Can't move right");
			newX = originalPosition.x + constrainRight;
		}
		if (transform.position.x < originalPosition.x - constrainLeft) {
			Debug.Log ("Can't move left");
			newX = originalPosition.x - constrainLeft;
		}
		if (transform.position.y > originalPosition.y + constrainUp) {
			Debug.Log ("Can't move up");
			newY = originalPosition.y + constrainUp;
		} 
		if (transform.position.y < originalPosition.y - constrainDown) {
			Debug.Log ("Can't move down");
			newY = originalPosition.y - constrainDown;
		} 
		Debug.Log ("Original transform " + transform.position);
		Debug.Log ("New X: " + newX);
		Debug.Log ("New Y: " + newY);
		transform.position = new Vector3 (newX, newY, transform.position.z);
		Debug.Log ("new transform " + transform.position);
	}
}
