using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBall : MonoBehaviour {
	float rate = 0.001f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Grow ();
	}

	void Grow() {
		transform.localScale = new Vector3(transform.localScale.x+rate, transform.localScale.y+rate, transform.localScale.z+rate); 
	}

	IEnumerator Damage(Transform coll) {
		yield return new WaitForSeconds (0.5f);
		if (coll.name == "Human") {
			coll.GetComponent<Human> ().DamageHuman (5);
		} 
		if (coll.name == "Alien") {
			coll.GetComponent<Alien> ().DamageAlien (5);
		}

	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.transform.tag == "Player") {
			StartCoroutine (Damage (coll.transform));
		}
	}
}
