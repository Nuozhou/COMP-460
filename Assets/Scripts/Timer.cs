using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {
	
	public static bool elevatorBrokenTimer = false;
	public float remainingTime = 30f;

	// Update is called once per frame
	void Update () {
		if (elevatorBrokenTimer) {
			StartCoroutine (elevatorSurvival ());
			remainingTime -= Time.deltaTime;
			Debug.Log ("Print time messsage");
			GameMaster.ShowTimerMessage ("Survive: " + Mathf.Floor (remainingTime % 60).ToString () + " seconds");
			//GameMessage.textMessage.text = Mathf.Floor (remainingTime % 60).ToString () + " seconds";
			//GameMessage.textMessage.enabled = true;
			if (Mathf.Floor (remainingTime % 60) == 0f) {
				elevatorBrokenTimer = false;
				remainingTime = 30f;
				GameMaster.CloseTimerMessage ();
				//GameMessage.textMessage.text = "";
				//GameMessage.textMessage.enabled = false;
			}
		}
	}

	public IEnumerator elevatorSurvival() {
		yield return new WaitForSeconds(30);
		GameObject.FindGameObjectWithTag("Elevator").GetComponent<Elevator> ().broken = false;
	}



}
