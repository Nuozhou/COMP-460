using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaEffector : MonoBehaviour {

	void OnTriggerStay2D(Collider2D col) {
		if (col.tag == "Player" && col.name == "Human") {
			if (Input.GetButtonDown ("HumanJump")) {
				GetComponent<AreaEffector2D> ().enabled = true;
				StartCoroutine (Disable ());
			}
		}
	}

	IEnumerator Disable() {
		yield return new WaitForSeconds (6f);
		GetComponent<AreaEffector2D> ().enabled = false;
	}
}
