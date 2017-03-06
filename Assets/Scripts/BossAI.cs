using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Seeker))]
public class BossAI : MonoBehaviour {

	public int health = 1000;
	// Use this for initialization
	public Transform target1;
	public Transform target2;
	private Transform target;
	public Transform bullet;

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

	public Transform BossPrefab;

	public float bulletSpeed = 100f;
	public float fireRate = 3f;
	private float fireTime = 0;

	private bool facingRight;

	public Transform firePoint;

	public LayerMask attackLayer;

	private int currentWaypoint = 0;

	public bool isRealBoss = true;
	public bool hasCopy = false;

	void Start () {

		//Initialization and find the target
		rb2d = GetComponent<Rigidbody2D> ();
		seeker = GetComponent<Seeker> ();
		AttackDist = 20f;
		UpdateTarget ();
		facingRight = true;

		//Find a path to the target
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		//start a new path to the target position and return the result to the OnpathComplete method
		StartCoroutine (UpdatePath ());


	}

	//Continuely update the target and start moving to the target
	IEnumerator UpdatePath() {
		UpdateTarget ();
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	//Update the target (find the closest player
	void UpdateTarget() {
		if (target1 == null && target2 == null) {
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

		if (path == null) {
			return;
		}

		/*
		float rand = Random.Range (0f, 1f);

		if (rand < 0.005f) {
			if (isRealBoss && !hasCopy) {
				StartCoroutine (MakeCopy ());
				hasCopy = true;
			}
		} 
		*/

		/*
		else if (rand < 0.01f) {
			if (GetComponent<SpriteRenderer> ().enabled == true) {
				StartCoroutine (TurnInvisible ());
			}
		}
		*/


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

		rb2d.AddForce (dir * 100, fMode);

		if (target.position.x > transform.position.x && !facingRight)
		{
			Flip();
		}

		else if (target.position.x < transform.position.x && facingRight)
		{
			Flip();
		}

		//Debug.Log ("Distance: " + Vector3.Distance (transform.position, target.position));
		if (Vector3.Distance (transform.position, target.position) < AttackDist) {
			Attack ();
		}

		float dist = Vector3.Distance (transform.position, path.vectorPath[currentWaypoint]);
		if (dist < nextWayPointDistance) {
			currentWaypoint++;
			return;
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;


		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void Attack() {
		if (fireRate == 0f) {
			Shoot ();
		} else {
			if (Time.time > fireTime) {
				fireTime = Time.time + 1f / fireRate;
				Shoot ();
			}
		}
	
	}

	void Shoot() {
		Vector2 direction = target.position - firePoint.position;
		direction.Normalize ();
		Transform bulletInstance = Instantiate (bullet, firePoint.position, Quaternion.Euler (new Vector3 (0, 0, 0)));
		bulletInstance.GetComponent<Rigidbody2D> ().velocity = direction * bulletSpeed;
	}

	void Blink(Vector3 targetPosition) {
		transform.position = new Vector3 (targetPosition.x, targetPosition.y, targetPosition.z);
	}

	void Spawn() {
		
	}

	IEnumerator TurnInvisible() {
		GetComponent<SpriteRenderer> ().enabled = false;
		yield return new WaitForSeconds (2f);
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	IEnumerator MakeCopy() {
		Transform bossCopy = Instantiate (BossPrefab, transform.position + new Vector3 (-20f, 10f, 0f), transform.rotation);
		bossCopy.GetComponent<BossAI> ().isRealBoss = false;
		bossCopy.GetComponent<BossAI> ().target1 = GameObject.Find ("Human").transform;
		bossCopy.GetComponent<BossAI> ().target2 = GameObject.Find ("Alien").transform;
		yield return new WaitForSeconds (5f);
		Destroy (bossCopy.gameObject);
		hasCopy = false;
	}

	public void Damage(int amount) {
		health -= amount;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}
}
