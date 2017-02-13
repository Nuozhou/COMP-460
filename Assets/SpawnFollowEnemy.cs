﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFollowEnemy : MonoBehaviour {

	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public GameObject enemyPrefab;

	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
		enemy.GetComponent<EnemyFollowTwo> ().target1 = GameObject.Find ("Human").transform;
		enemy.GetComponent<EnemyFollowTwo> ().target2 = GameObject.Find ("Alien").transform;
	}
}
