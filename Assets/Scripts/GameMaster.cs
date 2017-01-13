using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster gm;

	void Start() {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMaster>();
		}
	}

	public Transform HumanPrefab;
	public Transform SavePoint;

	public static void KillHuman(Human human) {
		human.gameObject.transform.position = gm.SavePoint.position;
		human.health = 100;
	}
}
