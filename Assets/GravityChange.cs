using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityChange : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float rand = Random.Range (0f, 1f);

		if (rand < 0.005f) {
			Physics2D.gravity *= -1;
		} 
	}
}
