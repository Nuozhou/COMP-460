using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRotate : MonoBehaviour {
	//public GameObject center;
	GameObject[] allObjects;
	List<Transform> rotateObjects = new List<Transform>();
	List<Transform> reverseRotateObjects = new List<Transform> ();
	public float speed = 5f; 
	private Vector3 zAxis = new Vector3(0, 0, 1);

	// Use this for initialization
	void Start () {
		allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject obj in allObjects) {
			if (obj.tag == "RotatePlane") {
				rotateObjects.Add (obj.transform);
			} else if (obj.tag == "ReverseRotatePlane") {
				reverseRotateObjects.Add (obj.transform);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform trans in rotateObjects) {
			trans.RotateAround (transform.position, zAxis, speed * Time.deltaTime);
		}

		foreach (Transform trans in reverseRotateObjects) {
			trans.RotateAround (transform.position, zAxis, -speed * Time.deltaTime);
		}
	}
}
