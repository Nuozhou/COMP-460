using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAlterObject : MonoBehaviour {

	public bool facingRight = false;
	public string objectName = "FurBallConst";
	GameObject SpawnObject;

//	Rigidbody2D rb2d;
//	float myWidth;
//	public float speed;

	// Use this for initialization
	void Start () {
//		rb2d = GetComponent<Rigidbody2D> ();
//		myWidth = GetComponent<SpriteRenderer> ().bounds.extents.x;
		SpawnObject = (GameObject)Instantiate(Resources.Load(objectName));
		InvokeRepeating ("Spawn", 0f, 20f);
	}


	void Spawn() {
		SpawnObject = Instantiate (SpawnObject, transform.position, transform.rotation);
		SpawnObject.GetComponent<enemySelfMovement> ().SetSpeed (0.1f);
	}

//	void FixedUpdate() {
//		Vector2 lineCast = transform.position - transform.right * myWidth;
//		Debug.DrawLine (lineCast, lineCast + Vector2.down);
//		bool isGround = Physics2D.Linecast (lineCast, lineCast + Vector2.down, enemyMask);
//
//		if (!isGround) {
//			Vector3 currRot = transform.eulerAngles;
//			currRot.y += 180;
//			transform.eulerAngles = currRot;
//		}
//
//		Vector2 myVel = rb2d.velocity;
//		myVel.x -=  transform.right.x * speed;
//		rb2d.velocity = myVel;
//	}


	// Update is called once per frame
	void Update () {
		
	}
}
