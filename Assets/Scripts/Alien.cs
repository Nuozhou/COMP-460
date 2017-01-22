using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {

	public int health = 100;

	public void DamageAlien(int damage) {
		health -= damage;
		if (health <= 0) {
			GameMaster.KillAlien (this);
		}
	}
}
