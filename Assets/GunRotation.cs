using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotation : MonoBehaviour {

	public bool reset;

	// Update is called once per frame
	void Update () {
		
		if (reset) {
			transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
			return;
		} 
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
		transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}
}
