using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMessage : MonoBehaviour {

	public static Text textMessage;
	public static bool checkPoint = false;
	public static bool dead = false;

	// Use this for initialization
	void Start () {
		textMessage = GetComponent<Text> ();
	}

	void Update () {
		if (dead) {
			StartCoroutine (ShowMessage ("You died", 3));
			dead = false;
		}
		if (checkPoint) {
			StartCoroutine (ShowMessage ("Game Saved", 3));
			checkPoint = false;
		} 
	}
	
	IEnumerator ShowMessage (string message, float delay) {
		textMessage.text = message;
		textMessage.enabled = true;
		yield return new WaitForSeconds(delay);
		textMessage.enabled = false;
	}
}
