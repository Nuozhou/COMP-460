using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderReset : MonoBehaviour {

	public void Reset() {
		gameObject.GetComponent<Collider2D> ().enabled = false;
	}
}
