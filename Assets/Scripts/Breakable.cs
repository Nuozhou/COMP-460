using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
	public int lifetime = 1;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void Damage(int i) {
		lifetime -= i;
		Debug.Log ("hit!");
		if (lifetime <= 0) {
			
			Destroy (gameObject);
		}
		
	}

}
