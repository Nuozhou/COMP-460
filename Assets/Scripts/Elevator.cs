using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	private Rigidbody2D m_Rigidbody2D;
	private float alternateTime = 10f;

	// Use this for initialization
	void Start () {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		StartElevatorUp ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > alternateTime) {
			SwitchDirection ();
			alternateTime += 10f;
		}
	}

	public void StopElevator() {
		m_Rigidbody2D.velocity = new Vector2 (0f, 0f);
	}

	public void StartElevatorUp() {
		m_Rigidbody2D.velocity = new Vector2 (0f, 0.3f);
	}

	public void StartElevatorDown() {
		m_Rigidbody2D.velocity = new Vector2 (0f, -0.3f);
	}

	public void SwitchDirection() {
		m_Rigidbody2D.velocity = new Vector2 (0f, -m_Rigidbody2D.velocity.y);
	}
}
