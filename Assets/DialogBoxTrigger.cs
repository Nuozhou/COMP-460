using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxTrigger : MonoBehaviour {

	public string messageLine1;
	public string messageLine2;
	public string messageLine3;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			string message = messageLine1 + "\n" + messageLine2 + "\n" + messageLine3;
			GameMaster.ShowDialogMessage (message);
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player") {
			GameMaster.CloseDialogPanel ();
		}
	}
}
