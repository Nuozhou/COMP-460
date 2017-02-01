using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGlaze : MonoBehaviour {

	public Transform target;
	Rigidbody2D targetRb2d;
	Rigidbody2D rb2d;
	private bool isFalling = false;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		targetRb2d = target.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		float dist = rb2d.position.x - targetRb2d.position.x;
		if (dist < 0.5 && dist > -0.5) {
			transform.GetComponent<Falling> ().startTime = true;
			isFalling = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (isFalling) {
			Destroy (gameObject);
			if (coll.gameObject.tag == "Player") {
				target.GetComponent<Human> ().DamageHuman (5);
			}
				
		}

	}
}
