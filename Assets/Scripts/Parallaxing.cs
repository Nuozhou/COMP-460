using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

	public List<Transform> backgroundObjects;
	public List<float> scales; 
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
		scales = new List<float>();
		for (int i = 0; i < backgroundObjects.Count; i++) {
			scales.Add(backgroundObjects [i].position.z * (-1));
		}

	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgroundObjects.Count; i++) {
			float parallax1 = (previousCameraPosition.x - cam.position.x) * scales [i];
			float parallax2 = (previousCameraPosition.y - cam.position.y) * scales [i];
			float targetPositionX = backgroundObjects [i].position.x + parallax1;
			float targetPositionY = backgroundObjects [i].position.y + parallax2;
			Vector3 targetPosition = new Vector3 (targetPositionX, targetPositionY, backgroundObjects [i].position.z);
			backgroundObjects [i].position = Vector3.Lerp (backgroundObjects [i].position, targetPosition, smoothing * Time.deltaTime);
		}

		previousCameraPosition = cam.position;

	}
}
