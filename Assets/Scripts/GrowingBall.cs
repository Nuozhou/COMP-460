using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBall : MonoBehaviour {
	float rate = 0.001f;

	// Update is called once per frame
	void Update () {
		Grow ();
	}

	void Grow() {
		transform.localScale = new Vector3(transform.localScale.x+rate, transform.localScale.y+rate, transform.localScale.z+rate); 
	}

	public IEnumerator DestroyAfterAwake() {
		yield return new WaitForSeconds (5f);
		Destroy (gameObject);
	}

	IEnumerator Damage(Transform coll) {
		yield return new WaitForSeconds (0.5f);
		if (coll.name == "Human") {
			coll.GetComponent<Human> ().DamageHuman (40);
		} 
		if (coll.name == "Alien") {
			coll.GetComponent<Alien> ().DamageAlien (40);
		}


	}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.transform.tag == "Player") {
			Destroy (gameObject);
			StartCoroutine (Damage (coll.transform));
		}
	}
}
