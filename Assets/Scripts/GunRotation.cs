using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {

	private HumanMovements humanMovements;

	void Start() {
		humanMovements = transform.root.GetComponent<HumanMovements> ();
	}

	// Update is called once per frame
	void Update () {
		
		float horizontal = Input.GetAxis ("HumanHorizontalR");
		float vertical = Input.GetAxis ("HumanVerticalR");
		//Debug.Log ("horizontal:" + horizontal);
		//Debug.Log ("vertical:" + vertical);

		float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg; 
		if (angle > 20f) {
			angle = 20f;
		}
		if (angle < -20f) {
			angle = -20f;
		}

		if (humanMovements.m_FacingRight) {
			transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
		} else {
			transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, -angle));
		}
	}
}
