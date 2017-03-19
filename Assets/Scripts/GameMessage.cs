using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMessage : MonoBehaviour {

	public static Text textMessage;

	// Use this for initialization
	void Start () {
		textMessage = GetComponent<Text> ();
	}

}
