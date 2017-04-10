using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRain : MonoBehaviour {

	public Transform rock; 
	public float xMin;
	public float xMax;

	void Start() {
		InvokeRepeating("Generate", 0f, 2f);
	}

	private void Generate() {
		Instantiate (rock, new Vector3 (Random.Range (xMin, xMax), transform.position.y, transform.position.z), Quaternion.identity);
	}

}
