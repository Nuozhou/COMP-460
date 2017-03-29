using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

	public GameObject laserPrefab;
	private LineRenderer line;
	//public Transform laserHit;
	private Vector2 laserOrigin;
	private Vector2 lastHitPoint;
	public Vector2 laserDirection = new Vector2(0, - 90);
	private bool reflected = false;
	public GameObject reflectedLaser;
	public AudioClip iceBreakClip;

	void Start() {
		line = transform.GetComponent<LineRenderer> ();
		line.enabled = true;
		line.useWorldSpace = true;
		line.sortingLayerName = "Foreground";
		laserOrigin = new Vector2 (transform.position.x, transform.position.y);
	}

	void Update() {
		
		RaycastHit2D hit = Physics2D.Raycast (transform.position, laserDirection, 1000);
		//laserHit.position = hit.point;
		line.SetPosition (0, transform.position);
		if (hit.collider == null) {
			if (lastHitPoint != null) {
				if (reflected) {
					DestroyAllReflections ();
					reflected = false;
				}
			}
			Vector3 endPoint = transform.position + new Vector3(laserDirection.x * 100, laserDirection.y * 100, 0);
			line.SetPosition (1, endPoint); 
		} else {
			if (lastHitPoint != null) {
				if (hit.point.x != lastHitPoint.x || hit.point.y != lastHitPoint.y) {
					if (reflected) {
						DestroyAllReflections ();
						reflected = false;
					}
				}
			}
			line.SetPosition (1, hit.point);
			lastHitPoint = hit.point;
		}
		if (hit.collider != null) {
			if (hit.collider.tag == "Player") {
				if (hit.collider.gameObject.name == "Human") {
					hit.collider.gameObject.GetComponent<Human> ().DamageHuman (1);
				} else if (hit.collider.gameObject.name == "Alien") {
					hit.collider.gameObject.GetComponent<Alien> ().DamageAlien (1);
				}
			} else if (hit.collider.tag == "LaserPlane") {
				if (!reflected) {

					Vector2 newDir = Vector2.Reflect ((hit.point - laserOrigin).normalized, hit.normal);
					reflectedLaser = Instantiate (laserPrefab, new Vector3 (hit.point.x, hit.point.y, 0), hit.transform.rotation);
					reflectedLaser.GetComponent<Laser> ().laserDirection = newDir;
					reflected = true;
				}
			} else if (hit.collider.tag == "LaserTarget") {
				Destroy (hit.transform.gameObject);
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	public void DestroyAllReflections() {
		GameObject laser = reflectedLaser;
		while (laser != null) {
			GameObject copy = laser;
			laser = laser.GetComponent<Laser> ().reflectedLaser;
			Destroy (copy);
		}
	}

}
