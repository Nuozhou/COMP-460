using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChangeDevice : MonoBehaviour {
	private Transform human;

	void Start() {
		human = GameObject.Find ("Human").transform;
	}
	
	// Update is called once per frame
	void Update () {
		bool operate = Input.GetButtonDown ("Operate");
		if (operate && Vector3.Distance(human.position, transform.position) < 15f) {
			if (Physics2D.gravity.y == -9.8f) {
				Physics2D.gravity = new Vector2 (0, 2f);
			} else {
				Physics2D.gravity = new Vector2 (0, -9.8f);
			}
		}
	}
}
