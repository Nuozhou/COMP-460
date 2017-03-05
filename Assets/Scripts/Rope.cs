using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

	public int vertextCount = 2;
	public float distance = 2;
	public GameObject nodePrefab;
	public List<GameObject> nodes = new List<GameObject>();
	public LineRenderer lr;
	public GameObject human;
	public GameObject lastNode;
	private Vector3 lastNodePosition;
	public bool humanAttached;
	private Animator anim;

	void Start() {
		lr = GetComponent<LineRenderer> ();
		human = GameObject.Find ("Human");
		anim = human.GetComponent<Animator> ();
		humanAttached = false;
		lastNode = gameObject;
		lastNodePosition = transform.position;
		nodes.Add (gameObject);
		for (int i = 0; i < vertextCount; i++) {
			GameObject node = Instantiate (nodePrefab, new Vector3 (lastNodePosition.x, lastNodePosition.y - distance, lastNodePosition.z), Quaternion.identity);
			lastNode.GetComponent<HingeJoint2D> ().connectedBody = node.GetComponent<Rigidbody2D> ();
			node.transform.SetParent (gameObject.transform);
			nodes.Add (node);
			lastNode = node;
			lastNodePosition = node.transform.position;
		}

		lastNode.GetComponent<HingeJoint2D> ().connectedBody = human.GetComponent<Rigidbody2D> ();
		lastNode.GetComponent<HingeJoint2D> ().enabled = false;

	}


	void Update() {
		lr.numPositions = vertextCount + 1;
		for (int i = 0; i < nodes.Count; i++) {
			lr.SetPosition (i, nodes [i].transform.position);
		}

		lastNodePosition = lastNode.transform.position;

		bool operate = Input.GetButtonDown ("Operate");
		if (operate && Vector3.Distance (human.transform.position, lastNodePosition) < 10f && humanAttached == false) {

			human.transform.position = new Vector3 (lastNodePosition.x, lastNodePosition.y - 1f, lastNodePosition.z);
			lastNode.GetComponent<HingeJoint2D> ().enabled = true;
			humanAttached = true;
			human.GetComponent<HumanMovements> ().attachedToRope = true;
			human.GetComponent<Human> ().attachedRope = this.transform;
			anim.SetBool ("UseRope", true);


		} else if (operate && humanAttached) {
			
			lastNode.GetComponent<HingeJoint2D> ().enabled = false;
			humanAttached = false;
			human.GetComponent<HumanMovements> ().attachedToRope = false;
			anim.SetBool ("UseRope", false);

		}
	}



}
