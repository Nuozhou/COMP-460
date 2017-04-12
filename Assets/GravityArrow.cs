using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityArrow : MonoBehaviour {
	private float previousGravityY = -9.8f;
	private Text gravityText;

	void Start () {
		gravityText = GameObject.Find ("GravityText").GetComponent<Text> ();
		gravityText.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		if (Physics2D.gravity.y != previousGravityY) {
			if (Physics2D.gravity.y < 0f) {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));
				StartCoroutine (showArrow ());
			} else {
				transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 180));
				StartCoroutine (showArrow ());
			}
			previousGravityY = Physics2D.gravity.y;
		}
	}

	IEnumerator showArrow() {
		gravityText.enabled = true;
		transform.GetComponent<Image> ().enabled = true;
		yield return new WaitForSeconds (5f);
		gravityText.enabled = false;
		transform.GetComponent<Image> ().enabled = false;
	}
}
