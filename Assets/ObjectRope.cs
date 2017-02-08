using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRope : MonoBehaviour {

	public int vertextCount = 2;
	public float distance = 2;
	public GameObject nodePrefab;
	public List<GameObject> nodes = new List<GameObject>();
	public LineRenderer lr;
	public Vector2 direction;
	private GameObject attachedObject;
	public GameObject attachedObjectPrefab;
	private GameObject lastNode;
	private Vector3 lastNodePosition;

	void Start() {
		lr = GetComponent<LineRenderer> ();
		lastNode = gameObject;
		lastNodePosition = transform.position;
		nodes.Add (gameObject);
		for (int i = 0; i < vertextCount; i++) {
			GameObject node = Instantiate (nodePrefab, new Vector3 (lastNodePosition.x + direction.x * distance, lastNodePosition.y + direction.y * distance, lastNodePosition.z), Quaternion.identity);
			lastNode.GetComponent<HingeJoint2D> ().connectedBody = node.GetComponent<Rigidbody2D> ();
			node.transform.SetParent (gameObject.transform);
			nodes.Add (node);
			lastNode = node;
			lastNodePosition = node.transform.position;
		}
		attachedObject = Instantiate (attachedObjectPrefab, lastNodePosition, Quaternion.identity);
		lastNode.GetComponent<HingeJoint2D> ().connectedBody = attachedObject.GetComponent<Rigidbody2D> ();
		//attachedObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 500);
	}


	void Update() {
		lr.numPositions = vertextCount + 1;
		for (int i = 0; i < nodes.Count; i++) {
			lr.SetPosition (i, nodes [i].transform.position);
		}
		//attachedObject.GetComponent<Rigidbody2D> ().AddForce (Vector2.right * 10);
	}
}
