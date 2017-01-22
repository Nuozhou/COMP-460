using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour {
	public bool isPushed;
	private float currentX;

	// Use this for initialization
	void Start () {
		currentX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (isPushed) {
			currentX = transform.position.x;
		} else {
			transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
		}
	}
}
