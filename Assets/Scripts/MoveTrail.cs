using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour {

	public float moveSpeed = 200f; 
	public Vector3 direction;
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (direction * Time.deltaTime * moveSpeed);
		Destroy (gameObject, 0.05f);
	}
}
