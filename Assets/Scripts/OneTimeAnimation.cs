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
		yield return new WaitForSeconds (2f);
		change = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(transform.position, target.position) < 40f) {
			anim.enabled = true;
			StartCoroutine (ResetChange ());
		}
		if (change) {
			Destroy(GetComponent<PolygonCollider2D>());
			gameObject.AddComponent<PolygonCollider2D>();
		}
	}
}
