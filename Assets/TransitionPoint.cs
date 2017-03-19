using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionPoint : MonoBehaviour {
	public int nextSceneNum;
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player" && col.name == "Human") {
			StartCoroutine (FadeOut ());
		}
	}

	IEnumerator FadeOut() {
		GameObject.Find ("BlackCanvas").GetComponent<FadeInOut> ().FadeOut();
		yield return new WaitForSeconds (2f);
		SceneManager.LoadScene (nextSceneNum);
	}
}
