using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointing : MonoBehaviour {

	public Transform target;

	void Update() {
		Vector3 targetDir = target.position - transform.position;
		float angle = Vector3.Angle(transform.position, target.position);
		if (target.position.y > transform.position.y) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
		} else if (target.position.y == transform.position.y) {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
		} else {
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, -angle));
		}
	}


}
