using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

	private Rigidbody2D m_Rigidbody2D;
	private bool enable = true;
	public bool broken = false;
	private GameObject human;

	// Use this for initialization
	void Start () {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		human = GameObject.Find ("Human");
	}
	
	// Update is called once per frame
	void Update () {
		if (broken == true) {
			StopElevator ();
		}

		bool operate = Input.GetButtonDown ("Operate");
		if (operate == true && enable == false && broken == false && Vector3.Distance (human.transform.position, transform.position) < 100f) {
			StartElevatorUp ();
			enable = true;
		} else if (operate == true && enable == true && broken == false && Vector3.Distance (human.transform.position, transform.position) < 100f) {
			StopElevator ();
			enable = false;
		}
	}

	public void StopElevator() {
		m_Rigidbody2D.velocity = new Vector2 (0f, 0f);
	}

	public void StartElevatorUp() {
		m_Rigidbody2D.velocity = new Vector2 (0f, 2f);
	}

	public void StartElevatorDown() {
		m_Rigidbody2D.velocity = new Vector2 (0f, -2f);
	}

	public void SwitchDirection() {
		m_Rigidbody2D.velocity = new Vector2 (0f, -m_Rigidbody2D.velocity.y);
	}
}
