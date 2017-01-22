using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPush : MonoBehaviour {
	public LayerMask boxMask;
	private GameObject box;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Push and Pull object
		Physics2D.queriesStartInColliders = false;
		RaycastHit2D hit= Physics2D.Raycast (transform.position, Vector2.right * transform.localScale.x, 1f, boxMask);

		if (hit.collider != null && hit.collider.gameObject.tag == "Pushable" && Input.GetButtonDown("HumanPushPull")) {

			box = hit.collider.gameObject;

			box.GetComponent<FixedJoint2D> ().enabled = true;
			box.GetComponent<Pushable> ().isPushed = true;
			box.GetComponent<FixedJoint2D> ().connectedBody = this.GetComponent<Rigidbody2D> ();
		} else if (Input.GetButtonUp ("HumanPushPull")) {
			box.GetComponent<FixedJoint2D> ().enabled = false;
			box.GetComponent<Pushable> ().isPushed = false;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

		Gizmos.DrawLine (transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * 1f);
	}
}
