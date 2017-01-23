using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerKey : MonoBehaviour {

	public int index = 1;
	public Transform lift;
	//Rigidbody2D rb2d;
	AudioSource audio;

	// Use this for initialization
	void Start () {
		//rb2d = GetComponent<Rigidbody2D> ();
		audio = GetComponent<AudioSource>();

	}

	void OnCollisionEnter2D(Collision2D coll) {
		//audio.PlayOneShot (audio.GetComponent<AudioClip> ());
		Debug.Log("collide!");
		audio.Play ();
		lift.GetComponent<TriggerLift> ().Lift (index);
	}



	// Update is called once per frame
	void Update () {
		
	}
}
