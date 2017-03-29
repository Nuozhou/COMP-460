using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compressee : MonoBehaviour {

	public Transform compresser;
	public float killRadius;
	public float humanCompressDistance;
	public float alienCompressDistance;

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Compressee") {
			compresser.GetComponent<Compresser> ().moveDirection = -1f;
			if (GetComponent<AudioSource>() != null) {
				GetComponent<AudioSource> ().Play ();
			}
		}

		if (coll.gameObject.tag == "Player") {
			if (coll.gameObject.name == "Human") {
				//Debug.Log ("Contacted human");
				//Debug.Log ("Distance: " + compresser.GetComponent<Compresser> ().distance);
				//Debug.Log ("KillDistance: " + Mathf.Abs (coll.gameObject.transform.position.y - transform.position.y));
				if (compresser.GetComponent<Compresser> ().isVertical) {
					if (compresser.GetComponent<Compresser> ().distance <= humanCompressDistance && Mathf.Abs (coll.gameObject.transform.position.x - transform.position.x) < killRadius) {
						compresser.GetComponent<Compresser> ().HurtHuman (coll.transform);
					}
				} else {
					if (compresser.GetComponent<Compresser> ().distance <= humanCompressDistance && Mathf.Abs (coll.gameObject.transform.position.y - transform.position.y) < killRadius) {
						compresser.GetComponent<Compresser> ().HurtHuman (coll.transform);
					}
				}
			} else if (coll.gameObject.name == "Alien") {
				//Debug.Log ("Contacted alien");
				//Debug.Log ("Distance: " + compresser.GetComponent<Compresser> ().distance);
				//Debug.Log ("KillDistance: " + Mathf.Abs (coll.gameObject.transform.position.y - transform.position.y));
				if (compresser.GetComponent<Compresser> ().isVertical) {
					if (compresser.GetComponent<Compresser> ().distance <= alienCompressDistance && Mathf.Abs (coll.gameObject.transform.position.x - transform.position.x) < killRadius) {
						compresser.GetComponent<Compresser> ().HurtAlien (coll.transform);

					}
				} else {
					if (compresser.GetComponent<Compresser> ().distance <= alienCompressDistance && Mathf.Abs (coll.gameObject.transform.position.y - transform.position.y) < killRadius) {
						compresser.GetComponent<Compresser> ().HurtAlien (coll.transform);
					}
				}
			}
		}
	} 
}
