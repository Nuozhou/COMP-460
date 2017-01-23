using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeAnimation : MonoBehaviour {

	bool change = true;
	public Transform target;
	Rigidbody2D rb2d;
	Rigidbody2D targetRb2d;
	Animator anim;
	// Use this for initialization
	void Start () {
		
		anim = GetComponent<Animator> ();
		anim.enabled = false;
		targetRb2d = target.GetComponent<Rigidbody2D> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}

	IEnumerator ResetChange() {
		yield return new WaitForSeconds (0.5f);
		change = false;
	}
	
	// Update is called once per frame
	void Update () {
		float dist = rb2d.position.x - targetRb2d.position.x;
		if (dist < 8 && dist > -8) {
			anim.enabled = true;
			StartCoroutine (ResetChange ());
		}
		if (change) {
			Destroy(GetComponent<PolygonCollider2D>());
			gameObject.AddComponent<PolygonCollider2D>();
		}
	}
}
