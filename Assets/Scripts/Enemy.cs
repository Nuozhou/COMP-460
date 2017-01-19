using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;
	// Use this for initialization
	public GameObject cloud;

	public void Damage(int healthDecrease) {
		health -= healthDecrease;
		if (health <= 0) {
			
			if (cloud == null) {
				cloud = (GameObject)Instantiate (Resources.Load ("Cloud"), transform.position, transform.rotation);
				Debug.Log ("instantiale!");
			} else {
				Instantiate (cloud);
			}
			Destroy (gameObject);

		}
	}
}
