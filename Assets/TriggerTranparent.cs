using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTranparent : MonoBehaviour {

	public Transform[] transparentObjects;
	public Transform[] colliderObjects;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player" && col.name == "Human") {
			for (int i = 0; i < transparentObjects.Length; i++) {
				SpriteRenderer sprite = transparentObjects [i].GetComponent<SpriteRenderer> ();
				sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a * 0.5f);
			}

			for (int i = 0; i < colliderObjects.Length; i++) {
				colliderObjects [i].GetComponent<Collider2D> ().enabled = true;
			}
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.tag == "Player" && col.name == "Human") {
			for (int i = 0; i < transparentObjects.Length; i++) {
				SpriteRenderer sprite = transparentObjects [i].GetComponent<SpriteRenderer> ();
				sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a * 2f);
			}
				
		}
	}


}
