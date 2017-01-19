using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySelfMovement : MonoBehaviour {

	public LayerMask enemyMask;
	Rigidbody2D rb2d;
	float myWidth;
	public float speed;

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		myWidth = GetComponent<SpriteRenderer> ().bounds.extents.x;
		//rb2d.velocity = Vector3 (10, 0, 0);

	}

	public void SetSpeed(float inputSpeed) {
		speed = inputSpeed;
	}

	void FixedUpdate() {
		Vector2 lineCast = transform.position - transform.right * myWidth;
		Debug.DrawLine (lineCast, lineCast + Vector2.down);
		bool isGround = Physics2D.Linecast (lineCast, lineCast + Vector2.down, enemyMask);

		if (!isGround) {
			Vector3 currRot = transform.eulerAngles;
			currRot.y += 180;
			transform.eulerAngles = currRot;
		}

		Vector2 myVel = rb2d.velocity;
		myVel.x -=  transform.right.x * speed;
		rb2d.velocity = myVel;
		//rb2d.velocity = rb2d.velocity.normalized * speed;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
