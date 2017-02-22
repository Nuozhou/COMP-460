using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectToPoint : MonoBehaviour {
	
	private Vector3 originalCameraPosition;
	private Vector3 currentCameraPosition;
	public Transform target;
	public float moveSpeed;
	private bool reachedTarget;
	public bool finishedDirecting;

	// Use this for initialization
	void Start () {
		originalCameraPosition = Camera.main.transform.position;
		currentCameraPosition = Camera.main.transform.position;
		reachedTarget = false;
		finishedDirecting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!reachedTarget) {
			currentCameraPosition = Vector3.Lerp (originalCameraPosition, target.transform.position, moveSpeed * Time.deltaTime);
		} else {
			currentCameraPosition = Vector3.Lerp (target.transform.position, originalCameraPosition, moveSpeed * Time.deltaTime);
		}
		transform.position = currentCameraPosition;

		if (currentCameraPosition == target.transform.position) {
			reachedTarget = true;
		}

		if (currentCameraPosition == originalCameraPosition && reachedTarget) {
			finishedDirecting = true;
		}
	}
}
