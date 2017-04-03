using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRotate : MonoBehaviour {
	//public GameObject center;
	GameObject[] allObjects;
	private Vector3 zAxis = new Vector3(0, 0, 1);

	// Use this for initialization
	void Start () {
		allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();

	}
	
	// Update is called once per frame
	void Update () {
		allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in allObjects) {
			if (obj.tag == "RotatePlane") {
				obj.transform.RotateAround (transform.position, zAxis, 10 * Time.deltaTime);
			} else if (obj.tag == "ReverseRotatePlane") {
				obj.transform.RotateAround (transform.position, zAxis, -10 * Time.deltaTime);
			}
		}
	}
}
