using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMaxDistChange : MonoBehaviour {

	public Transform[] compresserObjects;
	public float originalX;

	void Start() {
		originalX = transform.position.x;
	}

	void Update() {
		for (int i = 0; i < compresserObjects.Length; i++) {
			compresserObjects [i].GetComponent<Compresser> ().maxDistance = compresserObjects [i].GetComponent<Compresser> ().originalMaxDistance + (transform.position.x - originalX) * 2f;
		}
	}
}
