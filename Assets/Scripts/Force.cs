using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour {
	public GameObject arrow;
	public bool isGrabbed = false;
	public LayerMask grabMask;
	public float throwForce = 50f;
	public float grabRange = 5f;

	private GameObject grabbedObject;
	private GameObject arr;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("AlienGrab")) {
			if (!isGrabbed) {
				// Get all the colliders within the radius
				Collider2D[] colliderArray = 
					Physics2D.OverlapCircleAll (new Vector2 (transform.position.x, transform.position.y), 
						grabRange, grabMask);
				Debug.Log ("ColliderArrayLength: " + colliderArray.Length);
				// Calculate if there is any throwable objects.
				int numThrowableObjects = 0; 
				for (int i = 0; i < colliderArray.Length; i++) {
					if (colliderArray [i].gameObject.tag == "Throwable" || colliderArray [i].gameObject.tag == "Enemy") {
						numThrowableObjects++;
					}
				}
				Debug.Log ("numThrowable: " + numThrowableObjects);

				if (numThrowableObjects > 0) {
					isGrabbed = true;
					grabbedObject = colliderArray [0].gameObject;
					float minDistance = Vector2.Distance (new Vector2 (grabbedObject.transform.position.x, grabbedObject.transform.position.y),
						new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y));
					foreach (Collider2D col in colliderArray) {
						if (col.gameObject.tag == "Throwable" || col.gameObject.tag == "Enemy") {
							float distance = Vector2.Distance (new Vector2 (col.gameObject.transform.position.x, col.gameObject.transform.position.y),
								new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y));
							if (distance < minDistance) {
								minDistance = distance;
								grabbedObject = col.gameObject;
							}
						}
					}
					// Move the grabbed object to the front of the alien.
					grabbedObject.transform.position = new Vector3 (transform.position.x + 2f, transform.position.y, transform.position.z);
				
					arr = Instantiate (arrow, grabbedObject.transform.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
				}
			} 
		}

		if (isGrabbed) {
			// Update the position of the grabbed object every frame
			grabbedObject.transform.position = new Vector3 (transform.position.x + 2f, transform.position.y, transform.position.z);
			arr.transform.position = new Vector3 (transform.position.x + 2f, transform.position.y, transform.position.z);

		}


		if (Input.GetButtonDown ("AlienThrow")) {
			if (isGrabbed) {
				isGrabbed = false;
				grabbedObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (throwForce * Mathf.Cos (arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad), throwForce * Mathf.Sin (arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad));
				//grabbedObject.GetComponent<Rigidbody2D> ().AddForce(new Vector2 (throwForce * Mathf.Cos (arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad), throwForce * Mathf.Sin (arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad)));
				//Debug.Log ("Rotation angle in degree: " + arr.transform.FindChild ("ThrowArrow").eulerAngles.z);
				//Debug.Log ("Rotation angle in radians: " + arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad);
				//Debug.Log ("X velocity: " + throwForce * Mathf.Cos (arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad));
				//Debug.Log ("Y velocity: " + throwForce * Mathf.Sin (arr.transform.FindChild ("ThrowArrow").eulerAngles.z * Mathf.Deg2Rad));
				Destroy (arr);
			}
		}
	}
}
