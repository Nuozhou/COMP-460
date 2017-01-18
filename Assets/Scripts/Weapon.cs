using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Rigidbody2D bullet;
	public Transform firePoint;
	public float bulletSpeed = 30f;
	public float fireRate = 3f;

	private float fireTime = 0;
	private HumanMovements humanMovements;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild("FirePoint");
		humanMovements = transform.root.GetComponent<HumanMovements> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0f) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot ();
			}
		} else {
			if (Input.GetButtonDown ("Fire1") && Time.time > fireTime) {
				fireTime = Time.time + 1f / fireRate;
				Shoot ();
			}
		}
	}

	private void Shoot() {
		if (humanMovements.m_FacingRight) {
			Rigidbody2D bulletInstance = Instantiate(bullet, firePoint.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(bulletSpeed, 0);
		} else {
			Rigidbody2D bulletInstance = Instantiate(bullet, firePoint.position, Quaternion.Euler(new Vector3(0,0,0))) as Rigidbody2D;
			bulletInstance.velocity = new Vector2(-bulletSpeed, 0);
		}
	}
}
