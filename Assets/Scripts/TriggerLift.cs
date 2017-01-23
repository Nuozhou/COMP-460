using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLift : MonoBehaviour {

	int stage = 0;
	//Rigidbody2D rb2d;
	Vector3 originalPosition;


	// Use this for initialization
	void Start () {
		//rb2d = GetComponent<Rigidbody2D> ();
		originalPosition = transform.position;
	}

	public void Lift (int nextStage) {
		if (nextStage == (stage + 1)) {
			transform.position = new Vector3(transform.position.x, transform.position.y-1, transform.position.z);
			stage += 1;
		} else {
			transform.position = originalPosition;
			stage = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
