using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBackground : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -0.5f) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + 0.3f, transform.position.z);
		}
		
	}
}
