using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlane : MonoBehaviour {
	public Transform center;
	public float speed = 7f;
	
	// Update is called once per frame
	void Update () {
		if (transform.tag == "RotatePlane") {
			transform.RotateAround (center.position, new Vector3 (0, 0, 1), speed * Time.deltaTime);
		} else if (transform.tag == "ReverseRotatePlane") {
			transform.RotateAround (center.position, new Vector3 (0, 0, 1), -speed * Time.deltaTime);
		}
	}
}
