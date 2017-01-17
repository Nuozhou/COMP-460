using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Person : MonoBehaviour {

	public Transform player1, player2;
	public float minSizeY = 5f;

	void SetCameraPos() {
		Vector3 middle = (player1.position + player2.position) * 0.5f;

		transform.position = new Vector3(
			middle.x,
			middle.y,
			transform.position.z
		);
	}
		
	void Update() {
		SetCameraPos();
	}
}
