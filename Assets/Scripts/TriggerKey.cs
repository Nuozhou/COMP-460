using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKey : MonoBehaviour {

	public int index = 1;
	public Transform lift;
	//Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		//rb2d = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		lift.GetComponent<TriggerLift> ().Lift (index);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
