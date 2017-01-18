using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;
	// Use this for initialization


	public void Damage(int healthDecrease) {
		health -= healthDecrease;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
