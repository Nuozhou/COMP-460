using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingIce : MonoBehaviour {

	//bool isStepped = false;

	// Use this for initialization
	void Start () {
		transform.GetComponent<FollowPath> ().enable = false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		//isStepped = true;
		transform.GetComponent<FollowPath> ().enable = true;
	}

	void OnCollisionExit2D(Collision2D coll) {
		//isStepped = false;
		transform.GetComponent<FollowPath> ().enable = false;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
