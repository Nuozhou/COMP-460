﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour {

	public int health = 100;
	private SpriteRenderer healthBar;
	private SpriteRenderer healthBarOutline;
	public Vector3 healthScale;
	private float lastHitTime;
	public float repeatDamagePeriod = 2f;	
	public int fallBoundary = -50;
	public Dictionary<string, int> inventory = new Dictionary<string, int>();

	void Start() {
		health = 100;
		healthBar = GameObject.Find("HumanHealth").GetComponent<SpriteRenderer>();
		healthBarOutline = GameObject.Find("HumanHealthOutline").GetComponent<SpriteRenderer>();
		healthScale = healthBar.transform.localScale;
		UpdateHealthBar ();
	}

	void Update() {
		if (transform.position.y <= fallBoundary)
			DamageHuman(health);
	}

	public void HealHuman (int amount) {
		health += amount;
		if (health > 100) {
			health = 100;
		}
		UpdateHealthBar ();
	}
	public void DamageHuman(int damage) {
		health -= damage;
		UpdateHealthBar ();
		if (health <= 0) {
			GameMessage.dead = true;
			StartCoroutine(GameMaster.KillHuman(this));
		}
	}

	public void AddToInventory (string name) {
		if (inventory.ContainsKey (name)) {
			inventory [name] += 1;
		} else {
			inventory.Add (name, 1);
		}
	}

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);

		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1f, 1f);

		StartCoroutine(DisplayHealthBar());
	}

	public IEnumerator DisplayHealthBar() {
		healthBar.sortingLayerName = "Players";
		healthBarOutline.sortingLayerName = "Players";
		yield return new WaitForSeconds(3);
		healthBar.sortingLayerName = "Default";
		healthBarOutline.sortingLayerName = "Default";
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if (col.gameObject.tag == "Enemy") {
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				DamageHuman (10);
				lastHitTime = Time.time;
			}
		} else if (col.gameObject.tag == "FallingIce") {
			if (Time.time > lastHitTime + repeatDamagePeriod) {
				DamageHuman (20);
				lastHitTime = Time.time;

			}
		}
	}

}
