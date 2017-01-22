using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour {

	public Rigidbody2D rb2d;
	float timer = 0f;
	public bool startTime = false;
	public float waitTime = 0.5f;
	public bool isIcicle;

	public Animator anim;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (startTime) {
			timer += Time.fixedDeltaTime;
			if (timer > waitTime) {
				rb2d.isKinematic = false;
				if (anim != null) {
					anim.enabled = false;
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (!isIcicle) {
			startTime = true;
		} 

	}


	void Fall() {
			
	}
}
