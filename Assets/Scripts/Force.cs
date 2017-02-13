﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour {
	public GameObject arrow;
	public bool isGrabbed = false;
	public LayerMask grabMask;
	public float throwForce = 50f;
	public float grabRange = 5f;

	private GameObject grabbedObject;
	private float grabbedLocationOffsetX;
	public GameObject arr;
	private AlienMovements alienMovements;


	// Use this for initialization
	void Start () {
		alienMovements = transform.root.GetComponent<AlienMovements> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (grabbedObject == null) {
			isGrabbed = false;
			if (arr != null) {
				Destroy (arr);
			}
		}

		if (Input.GetButtonDown ("AlienThrow")) {
			if (isGrabbed) {
				isGrabbed = false;
				grabbedObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (throwForce * Mathf.Cos (arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad), throwForce * Mathf.Sin (arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad));
				Destroy (arr);
			}
		}

		if (Input.GetButtonDown ("AlienGrab")) {
			if (!isGrabbed) {
				// Get all the colliders within the radius
				Collider2D[] colliderArray = 
					Physics2D.OverlapCircleAll (new Vector2 (transform.position.x, transform.position.y), 
						grabRange, grabMask);
				// Calculate if there is any throwable objects.
				int numThrowableObjects = 0; 
				for (int i = 0; i < colliderArray.Length; i++) {
					if (colliderArray [i].gameObject.tag == "Throwable" || colliderArray [i].gameObject.tag == "Enemy" || colliderArray [i].gameObject.tag == "Grabbable") {
						numThrowableObjects++;
					}
				}

				if (numThrowableObjects > 0) {
					isGrabbed = true;
					grabbedObject = colliderArray [0].gameObject;
					float minDistance = Vector2.Distance (new Vector2 (grabbedObject.transform.position.x, grabbedObject.transform.position.y),
						                    new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y));
					foreach (Collider2D col in colliderArray) {
						if (col.gameObject.tag == "Throwable" || col.gameObject.tag == "Enemy" || col.gameObject.tag == "Grabbable") {
							float distance = Vector2.Distance (new Vector2 (col.gameObject.transform.position.x, col.gameObject.transform.position.y),
								                 new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y));
							if (distance < minDistance) {
								minDistance = distance;
								grabbedObject = col.gameObject;
							}
						}
					}

					grabbedLocationOffsetX = 5f;
					// Move the grabbed object to the front of the alien.
					grabbedObject.transform.position = new Vector3 (transform.position.x + grabbedLocationOffsetX, transform.position.y, transform.position.z);
				
					arr = Instantiate (arrow, grabbedObject.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
			} else {
				isGrabbed = false;
				Destroy (arr);
			}
		}

		if (isGrabbed) {
			grabbedObject.transform.position = new Vector3 (transform.position.x + grabbedLocationOffsetX, transform.position.y, transform.position.z);
			Debug.Log ("grabbed Object: " + grabbedObject.transform.position);
			arr.transform.position = new Vector3 (transform.position.x + grabbedLocationOffsetX, transform.position.y, transform.position.z);
		}
	}
}
