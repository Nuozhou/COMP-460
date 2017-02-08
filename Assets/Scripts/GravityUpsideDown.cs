using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityUpsideDown : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Physics2D.gravity *= -1;
		transform.Rotate (0, 0, 180, Space.World);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
