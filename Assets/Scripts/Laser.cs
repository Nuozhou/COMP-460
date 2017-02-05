using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	
	LineRenderer line;
	//public Transform laserHit;
	int n = 2;

	void Start() {
		line = transform.GetComponent<LineRenderer> ();
		line.enabled = true;
		line.useWorldSpace = true;
	}

	void Update() {
		
		RaycastHit2D hit = Physics2D.Raycast (transform.position, -transform.up);
		Debug.DrawLine (transform.position, hit.point);
		//laserHit.position = hit.point;
		line.SetPosition (0, transform.position);
		line.SetPosition (1, hit.point);
		if (hit.transform.tag == "Player") {
			hit.transform.GetComponent<Human> ().DamageHuman (5);
		}
	}

}
