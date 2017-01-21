using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	public int health = 100;

	[SerializeField]
	private Stat healthStat;
	// Use this for initialization
	public GameObject cloud;

	public Renderer rend;

	private Canvas healthCanvas;

	public void Start() {
		rend = GetComponent<Renderer>();
		healthStat.Initialize ();
		healthCanvas = GetComponentInChildren<Canvas> ();
		healthCanvas.enabled = false;

	}
	public void Damage(int healthDecrease) {
		if (!healthCanvas.isActiveAndEnabled) {
			StartCoroutine(ShowBar ());
		}
		healthStat.CurrentVal -= healthDecrease;
		if (healthStat.CurrentVal <= 0) {
			
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

	IEnumerator ShowBar() {
		healthCanvas.enabled = true;
		yield return new WaitForSeconds (1f);
		healthCanvas.enabled = false;
	}

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
