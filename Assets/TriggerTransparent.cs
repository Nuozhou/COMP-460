using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTransparent : MonoBehaviour {

	public bool entered = false;
	void OnTriggerEnter2D(Collider2D col) {
		Debug.Log ("Entered" + entered);
		if (!entered) {
			SpriteRenderer sprite = transform.GetComponent<SpriteRenderer> ();
			sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a * 0.5f);
			Debug.Log ("alpha value:" + sprite.color.a);
			entered = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		SpriteRenderer sprite = transform.GetComponent<SpriteRenderer> ();
		sprite.color = new Color (sprite.color.r, sprite.color.g, sprite.color.b, Mathf.Clamp01(sprite.color.a * 2f));
		entered = false;
	}


}
