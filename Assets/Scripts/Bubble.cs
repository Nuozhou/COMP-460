﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

	public GameObject water;
	public GameObject bubble;
	private float height;
	private float width;
	private Vector3 center;
	// Use this for initialization
	void Start () {
		if (water == null) {
			water =  GameObject.FindGameObjectsWithTag("Water")[0];
		}
		if (bubble == null) {
			bubble = GameObject.FindGameObjectsWithTag("Bubble")[0];
		}
		Collider2D coll = water.gameObject.GetComponent<BoxCollider2D>();
		center = coll.bounds.center;
		height = coll.bounds.size.y-2;
		width = coll.bounds.size.x-2;

		InvokeRepeating ("generateRandomBubble", 0f, 2f);
	}

	void generateRandomBubble() {
		Vector3 pos = new Vector3 (Random.Range (center.x-width/2, center.x+width/2), Random.Range (center.y-height/2, center.y+height/2), center.z);
		GameObject bubbleSpawn = Instantiate (bubble, pos, Quaternion.identity);
		StartCoroutine (deleteBubble (bubbleSpawn));
	}

	IEnumerator deleteBubble(GameObject bubbleSpawn) {
		yield return new WaitForSeconds (6f);
		Destroy (bubbleSpawn);
	}
	// Update is called once per frame

}
