using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocateAlien : MonoBehaviour {

	private Transform human;
	private Transform alien;
	private Vector3 lastHumanPosition;
	// Use this for initialization
	void Start () {
		human = GameObject.Find ("Human").transform;
		alien = GameObject.Find ("Alien").transform;
		lastHumanPosition = human.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (human.position, alien.position) > 30f) {
			//alien.Translate (new Vector3((human.position.x - alien.position.x) * Time.deltaTime, 
			//	(human.position.y - alien.position.y) * Time.deltaTime, 
			//	(human.position.z - alien.position.z) * Time.deltaTime));
			alien.Translate(human.position - lastHumanPosition);
		}

		if (Vector3.Distance (human.position, alien.position) > 40f) {
			//alien.Translate (new Vector3((human.position.x - alien.position.x) * Time.deltaTime, 
			//	(human.position.y - alien.position.y) * Time.deltaTime, 
			//	(human.position.z - alien.position.z) * Time.deltaTime));
			alien.position = new Vector3(human.position.x + 2f, human.position.y, human.position.z);
		}
		lastHumanPosition = human.transform.position;
	}
}
