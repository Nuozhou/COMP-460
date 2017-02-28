using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {


	public enum MoveType {UsePhysice, UseTransform};
	public MoveType MoveTypes;
	public Transform[] pathPoints;
	public int currentPath = 0;
	public float reachDistance = 5.0f;
	public float speed = 5.0f;
	Rigidbody2D rb2d;
	public bool enable = true;



	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (enable) {
			switch (MoveTypes) {
			case MoveType.UseTransform:
				UseTransform ();
				break;
			case MoveType.UsePhysice:
				UsePhysice ();
				break;
			}
		}

	}

	void UsePhysice() {
		Vector3 dir = pathPoints [currentPath].position - transform.position;
		Vector3 dirNorm = dir.normalized;
		rb2d.velocity = new Vector3 (dirNorm.x * (speed * Time.fixedDeltaTime), rb2d.velocity.y);
		if (dir.magnitude <= reachDistance) {
			currentPath++;
			if (currentPath >= pathPoints.Length) {
				currentPath = 0;
			}
		}
	}

	void UseTransform() {
		Vector3 dir = pathPoints [currentPath].position - transform.position;
		Vector3 dirNorm = dir.normalized;
		transform.Translate (dirNorm * (speed*Time.fixedDeltaTime));

		if (dir.magnitude <= reachDistance) {
			currentPath++;
			if (currentPath >= pathPoints.Length) {
				currentPath = 0;
			}
		}
	}

	void OnDrawGizmos() {
		if (pathPoints == null) {
			return;
		}
		foreach(Transform pathPoint in pathPoints) {
			if (pathPoint) {
				Gizmos.DrawSphere (pathPoint.position, reachDistance);
			}
		}
	}
}
