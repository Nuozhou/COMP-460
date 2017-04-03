using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMenuControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		EventSystem.current.SetSelectedGameObject (null);
		EventSystem.current.SetSelectedGameObject (transform.Find("MainMenuButton").gameObject);
	}
}
