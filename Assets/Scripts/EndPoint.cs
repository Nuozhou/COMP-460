using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player" && col.name == "Human") {
			SceneManager.LoadScene (2);
		}
	}
}
