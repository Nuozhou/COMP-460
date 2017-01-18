using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;
	// Use this for initialization
	void Start () {
		
	}

	void Damage(int healthDecrease) {
		health -= healthDecrease;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
