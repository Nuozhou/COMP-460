using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

	public Vector3 originalPosition;
	public float movableDistance;
	void Awake () {
		originalPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
