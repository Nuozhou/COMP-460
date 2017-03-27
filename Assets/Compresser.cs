using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compresser : MonoBehaviour {

	public Transform upperObject;
	public Transform lowerObject;
	private float maxDistance;
	public float moveSpeed;
	public float moveDirection;
	public float distance;

	void Start() {
		maxDistance = Mathf.Abs (upperObject.position.y - lowerObject.position.y);
		moveDirection = 1f;
	}

	void Update() {
		distance = Mathf.Abs (upperObject.position.y - lowerObject.position.y);
		if (distance >= maxDistance) {
			moveDirection = 1f;
		}
		upperObject.position = new Vector3 (upperObject.position.x, upperObject.position.y - moveSpeed * moveDirection, upperObject.position.z);
		lowerObject.position = new Vector3 (lowerObject.position.x, lowerObject.position.y + moveSpeed * moveDirection, lowerObject.position.z);
	}
}
