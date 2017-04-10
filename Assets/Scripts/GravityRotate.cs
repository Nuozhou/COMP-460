using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRotate : MonoBehaviour {
	//public GameObject center;
	GameObject[] allObjects;
	List<Transform> rotateObjects = new List<Transform>();
	public float speed = 5f; 
	private Vector3 zAxis = new Vector3(0, 0, 1);

	// Use this for initialization
	void Start () {
		allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in allObjects) {
			rotateObjects.Add (obj.transform);
		}

	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform trans in rotateObjects) {
			if (trans.tag == "RotatePlane") {
				trans.RotateAround (transform.position, zAxis, speed * Time.deltaTime);
			} else if (trans.tag == "ReverseRotatePlane") {
				trans.RotateAround (transform.position, zAxis, -speed * Time.deltaTime);
			}
		}
	}
}
