using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Human : MonoBehaviour {

	public int health = 100;
	private SpriteRenderer healthBar;
	private SpriteRenderer healthBarOutline;
	public Vector3 originalLocalScale;
	public Vector3 healthScale;
	private float lastHitTime;
	public float repeatDamagePeriod = 2f;	
	public int fallBoundary = -30;
	public Transform attachedRope;
	public AudioClip hurtClip;
	public AudioClip deathClip;
	public Dictionary<string, int> inventory = new Dictionary<string, int>();
	public bool humanDead = false;

	void Start() {
		health = 100;
		healthBar = GameObject.Find("HumanHealth").GetComponent<SpriteRenderer>();
		healthBarOutline = GameObject.Find("HumanHealthOutline").GetComponent<SpriteRenderer>();
		healthScale = healthBar.transform.localScale;
		originalLocalScale = transform.localScale;
		Debug.Log ("Health bar start");
		UpdateHealthBar ();
	}

	void Update() {
		if (transform.position.y <= fallBoundary && !humanDead) {
			DamageHuman (health);
		}
		Debug.Log ("Update Health bar layer: " + healthBar.sortingLayerName);
	}

	public void HealHuman (int amount) {
		health += amount;
		if (health > 100) {
			health = 100;
		}
		UpdateHealthBar ();
	}
	public void DamageHuman(int damage) {
		if (Time.time > lastHitTime + repeatDamagePeriod) {
			if (!humanDead) {
				GetComponent<AudioSource> ().clip = hurtClip;
				GetComponent<AudioSource> ().Play ();
			}
			lastHitTime = Time.time;
			health -= damage;
			if (health < 0) {
				health = 0;
			}
			UpdateHealthBar ();
			if (health <= 0 && !humanDead) {
				humanDead = true;
				GetComponent<AudioSource>().clip = deathClip;
				GetComponent<AudioSource> ().Play ();
				StartCoroutine(GameMaster.KillHuman(this));
			}
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
		Debug.Log ("healthbar layer: " + healthBar.sortingLayerName);
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		// If the colliding gameobject is an Enemy...
		if (col.gameObject.tag == "Enemy") {
			// ... and if the time exceeds the time of the last hit plus the time between hits...
			DamageHuman (10);
		} else if (col.gameObject.tag == "SwingRock") {
			DamageHuman (50);
		}
	}

}
