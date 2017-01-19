using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;
	// Use this for initialization
	public GameObject cloud;

	public Renderer rend;

	public void Start() {
		rend = GetComponent<Renderer>();
	}
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

		} else {
			StartCoroutine (Blink (8.0));
		}
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
