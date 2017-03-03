using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour {

	public Transform bossPrefab;

	public bool isTriggered = false;

	void OnTriggerEnter2D(Collider2D col) {
		if (!isTriggered) {
			if (col.tag == "Player") {
				Transform boss = Instantiate (bossPrefab, new Vector3 (transform.position.x + 10f, transform.position.y + 5f, transform.position.z), Quaternion.identity);
				boss.GetComponent<BossAI> ().target1 = GameObject.Find ("Human").transform;
				boss.GetComponent<BossAI> ().target1 = GameObject.Find ("Alien").transform;
				isTriggered = true;
			}
		} 
		/*
		Camera.main.GetComponent<Camera2Person> ().enabled = false;
		Camera.main.GetComponent<DirectToPoint> ().target = boss;
		Camera.main.GetComponent<DirectToPoint> ().enabled = true;
		while (!Camera.main.GetComponent<DirectToPoint> ().finishedDirecting) {
			Debug.Log ("Directing camera");
		}
		Camera.main.GetComponent<Camera2Person> ().enabled = true;
		Camera.main.GetComponent<DirectToPoint> ().enabled = false;
		*/

	}
}
