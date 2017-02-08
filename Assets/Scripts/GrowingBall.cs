using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingBall : MonoBehaviour {
	float rate = 0.001f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Grow ();
	}

	void Grow() {
		transform.localScale = new Vector3(transform.localScale.x+rate, transform.localScale.y+rate, transform.localScale.z+rate); 
	}
}
