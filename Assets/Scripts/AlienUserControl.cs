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

	void Update() {
		bool rotate = Input.GetButtonDown ("AlienOperate");
		if (rotate) {
			GameObject inner = GameObject.Find ("InnerRing");
			GameObject outer = GameObject.Find ("OuterRing");
			if (inner != null) {
				if (inner.tag == "RotatePlane") {
					inner.tag = "ReverseRotatePlane";
				} else {
					inner.tag = "RotatePlane";
				}
			}

			if (outer != null) {
				if (outer.tag == "RotatePlane") {
					outer.tag = "ReverseRotatePlane";
				} else {
					outer.tag = "RotatePlane";
				}
			}
		}
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
		float h = Input.GetAxis("AlienHorizontal");
		float v = Input.GetAxis("AlienVertical");
		m_Character.Move (h, v);
	}
}
