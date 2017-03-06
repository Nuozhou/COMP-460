using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGlaze : MonoBehaviour {

	public Transform target;
	Rigidbody2D targetRb2d;
	Rigidbody2D rb2d;
	private Animator anim;
	private bool isFalling = false;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		targetRb2d = target.GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		float dist = rb2d.position.x - targetRb2d.position.x;
		if (dist < 0.5 && dist > -0.5) {
			transform.GetComponent<Falling> ().startFalling = true;
			isFalling = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (isFalling) {
			if (coll.gameObject.tag == "Player") {
				target.GetComponent<Human> ().DamageHuman (20);
				anim.SetBool ("Break", true);
				StartCoroutine (WaitAndDestroy ());
			} else {
				anim.SetBool ("Break", true);
				StartCoroutine (WaitAndDestroy ());
			}
		}
	}

	private IEnumerator WaitAndDestroy() {
		yield return new WaitForSeconds(0.2f);
		Destroy(gameObject);
	}
}
