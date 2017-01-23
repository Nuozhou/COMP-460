using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour {

	public TurretAI turretAI;

	// Use this for initialization
	void Awake () {
		turretAI = gameObject.GetComponentInParent<TurretAI> ();
	}
	
	// Update is called once per frame
	void OnTriggerStay2D(Collider2D col) {
		if (col.tag == "Player") {
			turretAI.Attack ();
		}
	}
}
