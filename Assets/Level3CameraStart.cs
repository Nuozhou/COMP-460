using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3CameraStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.orthographicSize = 100;
		Camera.main.transform.position = new Vector3 (0, 280, -10);
		StartCoroutine (WaitAndDirectToPlayer ());
	}

	IEnumerator WaitAndDirectToPlayer() {
		while (Camera.main.orthographicSize > 20f) {
			yield return new WaitForSeconds (0.01f);
			Camera.main.orthographicSize -= 0.4f;
			Camera.main.transform.position = new Vector3 (Camera.main.transform.position.x - 0.05f, Camera.main.transform.position.y - 0.7f, -10);
		}
		GameObject.Find ("Human").transform.position = new Vector3(-10, 140, 0);
		GameObject.Find ("Alien").transform.position = new Vector3(-10, 143, 0);
		GameObject.Find ("InnerRing").GetComponent<RotatePlane> ().enabled = false;
		GetComponent<Camera2Person> ().enabled = true;

	}
}
