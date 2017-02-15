using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	public string ButtonMessage1;
	public string ButtonName;
	public string ButtonMessage2;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			GameMaster.gm.SavePoint = gameObject.transform;
			if (ButtonMessage1 != "" && ButtonName != "" && ButtonMessage2 != "") {
				StartCoroutine (GameMaster.ShowButtonMessage (ButtonMessage1, ButtonMessage2, ButtonName));
			}
		}
	}

}
