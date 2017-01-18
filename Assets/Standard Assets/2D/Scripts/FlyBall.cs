using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class FlyBall : MonoBehaviour {

	// Use this for initialization
	public Transform target1;
	public Transform target2;

	private Seeker seeker;
	public Path path;

	public float updateRate = 2f;

	public float speed = 300f;
	public ForceMode2D fMode;

	public bool pathIsEnded = false;

	public float nextWayPointDistance = 3;
	private Rigidbody2D rb2d;

	public float upForce = 200f;

	private int currentWaypoint = 0;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();

		seeker = GetComponent<Seeker> ();

		if (target1 == null && target2 == null) {
			Debug.LogError ("fly ball no target found!");
			return;
		}
		if (Vector3.Distance (transform.position, target1.position) <= Vector3.Distance (transform.position, target2.position)) {
			seeker.StartPath (transform.position, target1.position, OnPathComplete);
		} else {
			seeker.StartPath (transform.position, target2.position, OnPathComplete);
		}
		//start a new path to the target position and return the result to the OnpathComplete method

		StartCoroutine (UpdatePath ());


	}

	IEnumerator UpdatePath() {
		if (Vector3.Distance (transform.position, target1.position) <= Vector3.Distance (transform.position, target2.position)) {
			seeker.StartPath (transform.position, target1.position, OnPathComplete);
		} else {
			seeker.StartPath (transform.position, target2.position, OnPathComplete);
		}


		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	public void OnPathComplete(Path p) {
		Debug.Log("we found a path!");

		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) {


			rb2d.velocity = Vector2.zero;

			rb2d.AddForce (new Vector2 (0, upForce));
		}
	}

	void FixedUpdate() {
		if (target1 == null && target2 == null) {
			return;
		}

		if (path == null) {
			return;
		}

		if (currentWaypoint >= path.vectorPath.Count) {
			if (pathIsEnded) {
				return;
			}
			Debug.Log ("end of path reached!");
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
}
