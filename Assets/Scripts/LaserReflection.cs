using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReflection : MonoBehaviour {

	LineRenderer line;
	//public Transform laserHit;
	public Vector2 LaserDirection;
	public Vector2 LaserOrigin;

	public bool active = false;

	void Start() {
		line = transform.GetComponent<LineRenderer> ();
		line.enabled = true;
		line.useWorldSpace = true;
		LaserDirection = -transform.up;
		LaserOrigin = transform.position;
	}

	void Update() {
		if (active) {
			RaycastHit2D hit = Physics2D.Raycast (LaserOrigin, LaserDirection);
			Debug.DrawLine (LaserOrigin, hit.point);
			//laserHit.position = hit.point;
			line.SetPosition (0, LaserOrigin);
			line.SetPosition (1, hit.point);
			if (hit.transform.tag == "LaserTarget") {
				Destroy (hit.transform);
			} else if (hit.transform.tag == "LaserPlane") {
				//Debug.Log ("hit!");
				Vector2 newDir = Vector2.Reflect((hit.point-LaserOrigin).normalized, hit.normal);
				hit.transform.GetComponent<LaserReflection> ().BecomeAlive (newDir, hit.point);
			}
		}
		active = false;

	}

	void BecomeAlive(Vector2 dir, Vector2 orig) {
		//Debug.Log ("alive!");
		active = true;
		LaserDirection = dir;
		LaserOrigin = orig;
	}
}
