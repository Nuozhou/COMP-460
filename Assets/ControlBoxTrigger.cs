using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBoxTrigger : MonoBehaviour {
	public string message;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			GameMaster.ShowControlMessage (message);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player") {
			GameMaster.CloseControlPanel ();
		}
	}
}
