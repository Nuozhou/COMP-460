using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	public string itemName;

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			if (col.gameObject.name == "Human") {
				col.gameObject.GetComponent<Human> ().AddToInventory(itemName);
			} else if (col.gameObject.name == "Alien") {
				col.gameObject.GetComponent<Alien> ().AddToInventory(itemName);
			}

			// Destroy the rocket.
			Destroy (gameObject);
		} 
	}
}
