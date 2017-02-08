using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRoatePlane : MonoBehaviour {
	bool active = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (active) {
			if (coll.gameObject.tag == "RotatePlane") {

				coll.gameObject.GetComponent<Breakable> ().Damage (1);
				active = false;
			}
		}

	}
}
