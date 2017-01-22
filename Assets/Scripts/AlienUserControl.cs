using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (AlienMovements))]
public class AlienUserControl : MonoBehaviour {

	private AlienMovements m_Character;
	private bool grab;
	// Use this for initialization
	private void Awake () {
		m_Character = GetComponent<AlienMovements>();
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
		float h = Input.GetAxis("AlienHorizontal");
		float v = Input.GetAxis("AlienVertical");
		m_Character.Move (h, v);
	}
}
