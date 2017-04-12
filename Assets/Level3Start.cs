using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Start : MonoBehaviour {
	public string dialogMessage;

	// Use this for initialization
	void Start () {
		Debug.Log ("Entered level 3 start");
		StartCoroutine (StartingMessages ());
	}

	private IEnumerator StartingMessages() {
		Debug.Log ("Entered starting messages");
		GameMaster.ShowDialogMessage (dialogMessage + "\n");
		yield return new WaitForSeconds (5f);
		GameMaster.CloseDialogPanel ();
	}
}
