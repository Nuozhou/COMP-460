using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour {
	public GameObject bullet;

	public int health = 300;

	public float distanceHuman;
	public float distanceAlien;
	public float wakeRange = 8f;
	public float bulletSpeed = 100f;
	public float fireRate = 3f;

	public bool awake = false;
	public bool facingRight = true;
	public Transform humanTarget;
	public Transform alienTarget;
	public Transform shootPointLeft;
	public Transform shootPointRight;

	private Vector3 targetFocusedPosition;
	private float fireTime = 0;

	void Update() {
		RangeCheck ();

		if (targetFocusedPosition.x > transform.position.x) {
			facingRight = true;
		} else {
			facingRight = false;
		}

	}

	void RangeCheck() {
		distanceHuman = Vector3.Distance (transform.position, humanTarget.position);
		distanceAlien = Vector3.Distance (transform.position, alienTarget.position);
		if (Mathf.Min (distanceHuman, distanceAlien) <= wakeRange) {
			awake = true;
		} else {
			awake = false;
		}

		if (distanceHuman < distanceAlien) {
			targetFocusedPosition = humanTarget.position;
		} else {
			targetFocusedPosition = alienTarget.position;
		}

	}

	public void Damage(int amount) {
		health -= amount;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public void Attack() {
		if (fireRate == 0f) {
			Shoot ();
		} else {
			if (Time.time > fireTime) {
				fireTime = Time.time + 1f / fireRate;
				Shoot ();
			}
		}
	}

	private void Shoot() {


		if (facingRight) {
			Vector2 direction = targetFocusedPosition - shootPointRight.position;
			direction.Normalize ();
			GameObject bulletInstance = Instantiate(bullet, shootPointRight.position, Quaternion.Euler(new Vector3(0,0,0)));
			bulletInstance.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
		} else {
			Vector2 direction = targetFocusedPosition - shootPointLeft.position;
			direction.Normalize ();
			GameObject bulletInstance = Instantiate(bullet, shootPointLeft.position, Quaternion.Euler(new Vector3(0,0,0)));
			bulletInstance.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
		}

	}

}
