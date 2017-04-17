using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToCenter : MonoBehaviour {

	public Transform target; //Target to point at (you could set this to any gameObject dynamically)
	private Vector3 targetPos;
	private Vector3 screenMiddle;
	private Transform human;

	void Start() {
		human = GameObject.Find ("Human").transform;
	}


	void Update() {

		float y = target.position.y - human.position.y;
		float x = target.position.x - human.position.x;
		float tarAngle = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;
		transform.localRotation = Quaternion.Euler (0, 0, tarAngle);
		/*
		//Get the targets position on screen into a Vector3
		targetPos = Camera.main.WorldToScreenPoint (target.transform.position);
		//Get the middle of the screen into a Vector3
		screenMiddle = new Vector3 (Screen.width / 2, Screen.height / 2, 0); 
		//Compute the angle from screenMiddle to targetPos
		float tarAngle = (Mathf.Atan2 (targetPos.x - screenMiddle.x, Screen.height - targetPos.y - screenMiddle.y) * Mathf.Rad2Deg) + 90;
		if (tarAngle < 0)
			tarAngle += 360;

		transform.localRotation = Quaternion.Euler (0, 0, -tarAngle);


		//Calculate the angle from the camera to the target
		Vector3 camLocation = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
		Vector3 targetDir = target.transform.position - camLocation;
		float angle = Vector3.Angle (camLocation, target.transform.position);
		Debug.Log (angle);
		//If the angle exceeds 90deg inverse the rotation to point correctly
		if (angle < 90) {
			transform.localRotation = Quaternion.Euler (0, 0, -tarAngle);
		} else {
			transform.localRotation = Quaternion.Euler (0, 0, tarAngle);
		}
		*/



	}
}
