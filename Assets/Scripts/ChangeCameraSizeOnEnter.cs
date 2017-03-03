using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraSizeOnEnter : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			Camera.main.orthographicSize = 15f;
		}
	}
}
