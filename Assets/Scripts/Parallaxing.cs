using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public Transform[] backgroundObjects;
	private float[] scales; 
	public float smoothing;

	private Transform cam;
	private Vector3 previousCameraPosition;

	// Is called before Start()
	void Awake () {
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		previousCameraPosition = cam.position;
		scales = new float[backgroundObjects.Length];
		for (int i = 0; i < backgroundObjects.Length; i++) {
			scales [i] = backgroundObjects [i].position.z * (-1);
		}

	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgroundObjects.Length; i++) {
			float parallax = (previousCameraPosition.x - cam.position.x) * scales [i];
			float targetPositionX = backgroundObjects [i].position.x + parallax;
			Vector3 targetPosition = new Vector3 (targetPositionX, backgroundObjects [i].position.y, backgroundObjects [i].position.z);
			backgroundObjects [i].position = Vector3.Lerp (backgroundObjects [i].position, targetPosition, smoothing * Time.deltaTime);
		}

		previousCameraPosition = cam.position;

	}
}
