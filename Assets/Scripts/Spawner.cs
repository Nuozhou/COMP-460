using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
	public float spawnTime = 5f;		// The amount of time between each spawn.
	public float spawnDelay = 3f;		// The amount of time before spawning starts.
	public Transform[] pointList;
	public GameObject[] enemies;		// Array of enemy prefabs.

	void Start ()
	{
		// Start calling the Spawn function repeatedly after a delay .
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn ()
	{
		// Instantiate a random enemy.
		int enemyIndex = Random.Range(0, enemies.Length);
		GameObject enemy = Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
		enemy.GetComponent<FollowPath> ().pathPoints = pointList;
	}
}
