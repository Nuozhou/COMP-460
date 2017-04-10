using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour {

	public Rigidbody2D rb2d;
	//float timer = 0f;
	public bool startFalling = false;
	//public float waitTime = 0.5f;
	//public bool isIcicle;
	private Vector3 originalPosition;

	public Animator anim;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		//anim = GetComponent<Animator> ();
		originalPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		/*
		if (startTime) {
			timer += Time.fixedDeltaTime;
			if (timer > waitTime) {
				rb2d.isKinematic = false;
			}
		}
		*/
		if (startFalling) {
			StartCoroutine (Fall ());
		} else {
			rb2d.gravityScale = 0f;
			rb2d.velocity = Vector2.zero;
			transform.position = originalPosition;
		}

	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.collider.tag == "Player") {
			startFalling = true;
		}

	}

	public void Reset() {
		rb2d.gravityScale = 0f;
		startFalling = false;
		rb2d.velocity = Vector2.zero;
		transform.position = originalPosition;
	}


	private IEnumerator Fall() {
		yield return new WaitForSeconds(0.2f);
		rb2d.gravityScale = 3f;
	}
}
