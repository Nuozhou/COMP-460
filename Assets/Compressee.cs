using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compressee : MonoBehaviour {

	public Transform compresser;
	public float killRadius;
	public float humanCompressDistance;
	public float alienCompressDistance;

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("Detect collision");
		if (coll.gameObject.tag == "Compressee") {
			compresser.GetComponent<Compresser> ().moveDirection = -1f;
		}

		if (coll.gameObject.tag == "Player") {
			if (coll.gameObject.name == "Human") {
				if (compresser.GetComponent<Compresser> ().distance <= humanCompressDistance && Mathf.Abs(coll.gameObject.transform.position.x - transform.position.x) < killRadius) {
					coll.gameObject.GetComponent<Human> ().DamageHuman (100);
				}
			} else if (coll.gameObject.name == "Alien") {
				//Debug.Log ("Contacted alien");
				//Debug.Log ("Distance: " + compresser.GetComponent<Compresser> ().distance);
				//Debug.Log ("KillDistance: " + Mathf.Abs (coll.gameObject.transform.position.x - transform.position.x));
				if (compresser.GetComponent<Compresser> ().distance <= alienCompressDistance && Mathf.Abs(coll.gameObject.transform.position.x - transform.position.x) < killRadius) {
					coll.gameObject.GetComponent<Alien> ().DamageAlien (100);
				}
			}
		}
	} 
}
