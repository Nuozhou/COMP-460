using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Person : MonoBehaviour {

	public Transform target1;
	public Transform target2;
	public float damping = 1;
	public float lookAheadFactor = 3;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;

	private float m_OffsetZ;
	private Vector3 lastCenterPosition;
	private Vector3 m_CurrentVelocity;
	private Vector3 lookAheadPos;

	void Start() {
		if (target2 == null) {
			lastCenterPosition = target1.position;
		} else {
			lastCenterPosition = (target1.position + target2.position) * 0.5f;
		}
		m_OffsetZ = (transform.position - lastCenterPosition).z;
		transform.parent = null;
	}

	void SetCameraPos() {
		Vector3 middle;
		if (target2 == null) {
			middle = target1.position;
		} else {
			middle = new Vector3 ((target1.position.x + target2.position.x) * 0.5f, 
				                (target1.position.y + target2.position.y) * 0.5f, 0);
		}

		transform.position = new Vector3(middle.x, middle.y, transform.position.z);
	}
		
	void Update() {
		// only update lookahead pos if accelerating or changed direction
		Vector3 centerPosition;
		if (target2 == null) {
			centerPosition = target1.position;
		} else {
			centerPosition = (target1.position + target2.position) * 0.5f;
		}
		float xMoveDelta = (centerPosition - lastCenterPosition).x;

		bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget)
		{
			lookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
		}
		else
		{
			lookAheadPos = Vector3.MoveTowards(lookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
		}

		Vector3 aheadTargetPos = centerPosition + lookAheadPos + Vector3.forward*m_OffsetZ;
		Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

		transform.position = newPos;

		lastCenterPosition = centerPosition;
	}
}
