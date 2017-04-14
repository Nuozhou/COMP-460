using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Start : MonoBehaviour {
	public string dialogMessage;

	// Use this for initialization
	void Start () {
		StartCoroutine (StartingMessages ());
	}

	private IEnumerator StartingMessages() {
		GameMaster.ShowDialogMessage (dialogMessage + "\n");
		yield return new WaitForSeconds (5f);
		GameMaster.CloseDialogPanel ();
	}
}
