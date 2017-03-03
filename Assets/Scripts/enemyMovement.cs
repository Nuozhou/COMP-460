using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {


	public float enemySpeed;
	bool canFlip = true;
	public GameObject enemyGraphic;
	bool facingRight = false;
	float flipTime = 5f;
	float nextFlipChance = 0f;

	float chargingDist = 10f;



	public Transform target;

	public float chargeTime;
	float startChargeTime;
	bool charging = false;
	Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < nextFlipChance) { 
			if (Random.Range (0, 10) >= 5) {
				//flipFacing ();
				nextFlipChance = Time.time + flipTime;
			}
		}
		if (Vector3.Distance (transform.position, target.position) < chargingDist) {
			if (!charging)
				enterCharging ();
		} else {
			if (charging)
				exitCharging ();
		}
	}

	void enterCharging() {
		charging = true;
//		if (facingRight && target.position.x < transform.position.x)
//			flipFacing ();
//		else if (!facingRight && target.position.x > transform.position.x)
//			flipFacing ();
		canFlip = false;
		charging = true;
		startChargeTime = Time.time + chargeTime;
		stayCharging ();
	}

	void stayCharging() {
		if (Time.time >= startChargeTime) {
			if (!facingRight)
				rb.AddForce (new Vector2 (-1, 0) * enemySpeed);
			else
				rb.AddForce (new Vector2 (1, 0) * enemySpeed);
		}
	}

	void exitCharging() {
		canFlip = true;
		charging = false;
		rb.velocity = new Vector2 (0f, 0f);

	}

//	void OnTriggerEnter2D (Collider2D other) {
//		if (other.tag == "Player") {
//			Debug.Log ("enter 2d");
//			if (facingRight && other.transform.position.x < transform.position.x)
//				flipFacing ();
//			else if (!facingRight && other.transform.position.x > transform.position.x)
//				flipFacing ();
//			canFlip = false;
//			charging = true;
//			startChargeTime = Time.time + chargeTime;
//
//		}
//	}
//
//	void OnTriggerStay2D (Collider2D other) {
//		if (other.tag == "Player") {
//			if (Time.time >= startChargeTime) {
//				if (!facingRight)
//					rb.AddForce (new Vector2 (-1, 0) * enemySpeed);
//				else 
//					rb.AddForce (new Vector2 (1, 0) * enemySpeed);
//				
//			}
//		}
//	}
//
//	void OnTriggerExit2D (Collider2D other) {
//		if (other.tag == "Player") {
//			canFlip = true;
//			charging = false;
//			rb.velocity = new Vector2 (0f, 0f);
//
//		}
//	}


	void flipFacing() {
		if (!canFlip)
			return;
		float facingX = enemyGraphic.transform.localScale.x;
		facingX -= 1f;
		enemyGraphic.transform.localScale = new Vector3 (facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
		facingRight = !facingRight;
		GameObject.Find ("EnemyHealthDisplay").transform.localScale = GameObject.Find ("EnemyHealthDisplay").transform.localScale * (-1);

	}
}
