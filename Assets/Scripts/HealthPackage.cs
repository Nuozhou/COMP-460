using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackage : MonoBehaviour {

	public int healthBonus = 25;

	void OnTriggerEnter2D (Collider2D other)
	{
		// If the player enters the trigger zone...
		if (other.tag == "Player") {
			if (other.gameObject.name == "Human") {
				Human human = other.GetComponent<Human> ();
				human.HealHuman (healthBonus);
			} else if (other.gameObject.name == "Alien") {
				Alien alien = other.GetComponent<Alien> ();
				alien.HealAlien (healthBonus);
			}

			// Destroy the crate.
			Destroy (gameObject);
		}
	}
}
