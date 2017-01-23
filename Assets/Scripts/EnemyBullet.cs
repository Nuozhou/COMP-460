using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	public int damage = 10;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 2);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if(col.tag == "Player")
		{
			if (col.gameObject.name == "Human") {
				col.gameObject.GetComponent<Human> ().DamageHuman (damage);
			} else if (col.gameObject.name == "Alien") {
				col.gameObject.GetComponent<Alien> ().DamageAlien (damage);
			}
				
			Destroy (gameObject);
		}
	}
}
