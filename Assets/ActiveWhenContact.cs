using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWhenContact : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.transform.name == "Human") {
			GameObject.Find("InnerRing").GetComponent<RotatePlane> ().enabled = true;
		}
	}
}
