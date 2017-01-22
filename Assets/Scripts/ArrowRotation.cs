using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRotation : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float horizontal = Input.GetAxis ("AlienHorizontalR");
		float vertical = Input.GetAxis ("AlienVerticalR");
		//Debug.Log ("horizontal:" + horizontal);
		//Debug.Log ("vertical:" + vertical);

		float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg; 
		transform.parent.rotation = Quaternion.Euler (new Vector3 (0, 0, angle));
	}
}
