using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compresser : MonoBehaviour {

	public Transform upperObject;
	public Transform lowerObject;
	public float maxDistance;
	public float moveSpeed;
	public float moveDirection;
	public float originalMaxDistance;
	public float distance;
	public bool isVertical = true;

	void Start() {
		if (isVertical) {
			originalMaxDistance = Mathf.Abs (upperObject.position.y - lowerObject.position.y);
			maxDistance = Mathf.Abs (upperObject.position.y - lowerObject.position.y);
		} else {
			originalMaxDistance = Mathf.Abs (upperObject.position.x - lowerObject.position.x);
			maxDistance = Mathf.Abs (upperObject.position.x - lowerObject.position.x);
		}
		moveDirection = 1f;
	}

	void Update() {
		if (isVertical) {
			distance = Mathf.Abs (upperObject.position.y - lowerObject.position.y);
		} else {
			distance = Mathf.Abs (upperObject.position.x - lowerObject.position.x);
		}
		if (distance >= maxDistance) {
			moveDirection = 1f;
		}
		if (isVertical) {
			upperObject.position = new Vector3 (upperObject.position.x, upperObject.position.y - moveSpeed * moveDirection, upperObject.position.z);
			lowerObject.position = new Vector3 (lowerObject.position.x, lowerObject.position.y + moveSpeed * moveDirection, lowerObject.position.z);
		} else {
			upperObject.position = new Vector3 (upperObject.position.x + moveSpeed * moveDirection, upperObject.position.y, upperObject.position.z);
			lowerObject.position = new Vector3 (lowerObject.position.x - moveSpeed * moveDirection, lowerObject.position.y, lowerObject.position.z);
		}
	}

	public void HurtHuman(Transform human) {
		if (isVertical) {
			if (human.position.y < upperObject.position.y && human.position.y > lowerObject.position.y) {
				human.GetComponent<Human> ().DamageHuman (100);
				human.localScale = new Vector3 (human.localScale.x, human.localScale.y * 0.3f, human.localScale.z);
			}
		} else {
			if (human.position.x > upperObject.position.x && human.position.x < lowerObject.position.x) {
				human.GetComponent<Human> ().DamageHuman (100);
				human.localScale = new Vector3 (human.localScale.x * 0.3f, human.localScale.y, human.localScale.z);
			}
		}
	}

	public void HurtAlien(Transform alien) {
		if (isVertical) {
			if (alien.position.y < upperObject.position.y && alien.position.y > lowerObject.position.y) {
				alien.GetComponent<Alien> ().DamageAlien (100);
				alien.localScale = new Vector3 (alien.localScale.x, alien.localScale.y * 0.3f, alien.localScale.z);
			}
		} else {
			if (alien.position.x > upperObject.position.x && alien.position.x < lowerObject.position.x) {
				alien.GetComponent<Alien> ().DamageAlien (100);
				alien.localScale = new Vector3 (alien.localScale.x * 0.3f, alien.localScale.y, alien.localScale.z);
			}
		}
	}


}
