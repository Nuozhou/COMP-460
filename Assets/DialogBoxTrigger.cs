using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogBoxTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Player") {
			GameMaster.ShowDialogMessage ("This is dialog message" + "\n" + "and its second line");
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player") {
			GameMaster.CloseDialogPanel ();
		}
	}
}
