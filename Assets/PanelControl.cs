using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelControl : MonoBehaviour {

	private bool activePanel = false;

	// Update is called once per frame
	void Update () { 
		if (GameObject.Find ("Background").transform.position.y >= -0.5f && !activePanel) {
			transform.Find ("MainPanel").gameObject.SetActive (true);
			transform.Find ("CrossImage").gameObject.SetActive (true);
			transform.Find ("Text1").gameObject.SetActive (true);
			transform.Find ("Text2").gameObject.SetActive (true);
			EventSystem.current.SetSelectedGameObject (null);
			EventSystem.current.SetSelectedGameObject (transform.Find("MainPanel").Find("NewGameButton").gameObject);
			activePanel = true;
		}
	}
}
