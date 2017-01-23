using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowCrystal : MonoBehaviour {

	public bool isColide = false;
	Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisonEnter2D(Collision2D coll) {
		isColide = true;
		coll.gameObject.GetComponent<Human> ().DamageHuman (10);
	}
}
