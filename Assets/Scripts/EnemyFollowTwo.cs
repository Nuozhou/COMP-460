using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class EnemyFollowTwo : MonoBehaviour {

	// Use this for initialization
	public Transform target1;
	public Transform target2;
	private Transform target;

	private Seeker seeker;
	public Path path;

	public float updateRate = 2f;

	public float AttackDist;

	public int AttackPower;

	public float speed = 300f;
	public ForceMode2D fMode;

	public bool pathIsEnded = false;

	public float nextWayPointDistance = 3;
	private Rigidbody2D rb2d;



	private int currentWaypoint = 0;

	void Start () {

		//Initialization and find the target
		rb2d = GetComponent<Rigidbody2D> ();
		seeker = GetComponent<Seeker> ();
		AttackDist = 3 * GetComponent<SpriteRenderer> ().bounds.extents.x;
		UpdateTarget ();

		//Find a path to the target
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		//start a new path to the target position and return the result to the OnpathComplete method
		StartCoroutine (UpdatePath ());


	}

	//Update the target (find the closest player
	void UpdateTarget() {
		if (target1 == null && target2 == null) {
			Debug.LogError ("fly ball no target found!");
			return;
		}
		if (target1 == null) {
			target = target2;
		} else if (target2 == null) {
			target = target1;
		} else {
			if (Vector3.Distance (transform.position, target1.position) <= Vector3.Distance (transform.position, target2.position)) {
				target = target1;
			} else {
				target = target2;
			}
		}
	}

	//Continuely update the target and start moving to the target
	IEnumerator UpdatePath() {
		UpdateTarget ();
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	//Find the path
	public void OnPathComplete(Path p) {
		//Debug.Log("we found a path!");

		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	// Update is called once per frame
	void FixedUpdate() {
		if (target1 == null && target2 == null) {
			return;
		}
		if (Vector3.Distance (transform.position, target.position) < AttackDist) {
			Attack ();
		}

		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			//Debug.Log ("end of path reached!");
			pathIsEnded = true;
			return;

		} else {
			pathIsEnded = false; 
		}

		Vector3 dir = (path.vectorPath [currentWaypoint] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		rb2d.AddForce (dir, fMode);

		float dist = Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]);
		if (dist < nextWayPointDistance) {
			currentWaypoint++;
			return;
		}
	}

	IEnumerator Attack() {
		yield return new WaitForSeconds (0.5f);
		target.GetComponent<Human> ().DamageHuman (AttackPower);

	}
}
