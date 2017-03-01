using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public Rigidbody2D bullet;
	public Transform firePoint;
	public float bulletSpeed = 30f;
	public float fireRate = 3f;
	private Animator m_Anim;
	private bool isShooting;
	private int counter;

	private float fireTime = 0;
	private HumanMovements humanMovements;

	// Use this for initialization
	void Awake () {
		firePoint = transform.FindChild("FirePoint");
		humanMovements = transform.root.GetComponent<HumanMovements> ();
		m_Anim = GameObject.Find ("Human").GetComponent<Animator> ();
		//transform.GetComponent<SpriteRenderer> ().enabled = false;
		isShooting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0f) {
			if (Input.GetButton ("Fire1")) {
				m_Anim.SetBool ("UseGun", true);
				Shoot ();
			}
		} else {
			if (Input.GetButton ("Fire1") && Time.time > fireTime) {
				fireTime = Time.time + 1f / fireRate;
				m_Anim.SetBool ("UseGun", true);
				Shoot ();
			}
		}

		if (isShooting && counter < 30) {
			counter++;
		}

		if (isShooting && counter == 30) {
			counter = 0;
			isShooting = false;
			m_Anim.SetBool ("UseGun", false);
			//transform.GetComponent<SpriteRenderer> ().enabled = false;
		}

	}

	private void Shoot() {
		isShooting = true;
		counter = 0;
		GetComponent<AudioSource> ().Play ();
		//transform.GetComponent<SpriteRenderer> ().enabled = true;
		if (humanMovements.m_FacingRight) {
			Rigidbody2D bulletInstance = Instantiate(bullet, firePoint.position, Quaternion.Euler (transform.parent.eulerAngles)) as Rigidbody2D;
			bulletInstance.velocity = new Vector2 (bulletSpeed * Mathf.Cos (transform.parent.eulerAngles.z * Mathf.Deg2Rad), bulletSpeed * Mathf.Sin (transform.parent.eulerAngles.z * Mathf.Deg2Rad));
		} else {
			Rigidbody2D bulletInstance = Instantiate(bullet, firePoint.position, Quaternion.Euler (transform.parent.eulerAngles)) as Rigidbody2D;
			bulletInstance.velocity = new Vector2 (-bulletSpeed * Mathf.Cos (transform.parent.eulerAngles.z * Mathf.Deg2Rad), -bulletSpeed * Mathf.Sin (transform.parent.eulerAngles.z * Mathf.Deg2Rad));
			Vector3 theScale = bulletInstance.transform.localScale;
			theScale.x *= -1;
			bulletInstance.transform.localScale = theScale;
		}
	}
}
