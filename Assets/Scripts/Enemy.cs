using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	public int health = 100;

	//private Stat healthStat;
	private Vector3 healthScale;
	// Use this for initialization
	public GameObject cloud;


	public Renderer rend;

	//private Canvas healthCanvas;

	private SpriteRenderer healthBar;
	private SpriteRenderer healthBarOutline;

	public void Start() {
		rend = GetComponent<Renderer>();
		//healthStat.Initialize ();
		healthBar = transform.Find("EnemyHealthDisplay/EnemyHealth").GetComponent<SpriteRenderer>();
		healthBarOutline = transform.Find("EnemyHealthDisplay/EnemyHealthOutline").GetComponent<SpriteRenderer>();
		healthScale = healthBar.transform.localScale;
		//healthCanvas = GetComponentInChildren<Canvas> ();
		//healthCanvas.enabled = false;

	}
	public void Damage(int healthDecrease) {
//		if (!healthCanvas.isActiveAndEnabled) {
//			StartCoroutine(ShowBar ());
//		}

		//healthStat.CurrentVal -= healthDecrease;
		health -= healthDecrease;
		UpdateHealthBar ();

		if (health <= 0) {
			
			if (cloud == null) {
				cloud = (GameObject)Instantiate (Resources.Load ("Cloud"), transform.position, transform.rotation);
				Debug.Log ("instantiale!");
			} else {
				Instantiate (cloud);
			}
			Destroy (gameObject);

		} else {
			StartCoroutine (Blink (8.0));
		}
	}

	public void UpdateHealthBar ()
	{
		// Set the health bar's colour to proportion of the way between green and red based on the player's health.


		// Set the scale of the health bar to be proportional to the player's health.
		healthBar.transform.localScale = new Vector3(healthScale.x * health  * 0.01f, 1f, 1f);

		StartCoroutine(DisplayHealthBar());
	}

	public IEnumerator DisplayHealthBar() {
		healthBar.sortingLayerName = "Enemy";
		healthBarOutline.sortingLayerName = "Enemy";
		yield return new WaitForSeconds(3);
		healthBar.sortingLayerName = "Default";
		healthBarOutline.sortingLayerName = "Default";
	}

//	IEnumerator ShowBar() {
//		healthCanvas.enabled = true;
//		yield return new WaitForSeconds (1f);
//		healthCanvas.enabled = false;
//	}

	IEnumerator Blink(double time) {
		double endTime = Time.time + time;
//		while (Time.time < endTime) {
		int counter = 0;



		rend.enabled = false;
		yield return new WaitForSeconds(0.1f);
		rend.enabled = true;



			


		//}
	}
}
