using System;
using UnityEngine;

[RequireComponent(typeof (HumanMovements))]
public class HumanUserControl : MonoBehaviour {

	private HumanMovements m_Character;
	private bool m_Jump;
	private bool crouch;

	private GameObject box;


	private void Awake()
	{
		m_Character = GetComponent<HumanMovements>();
	}


	private void Update()
	{
		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			m_Jump = Input.GetButtonDown ("HumanJump");
			crouch = Input.GetButton ("HumanCrouch");
		}
	}


	private void FixedUpdate()
	{
		// Read the inputs.
		//bool crouch = Input.GetKey(KeyCode.LeftControl);
		float h = Input.GetAxis("HumanHorizontal");
		// Pass all parameters to the character control script.
		m_Character.Move(h, crouch, m_Jump);
		m_Jump = false;
	}
}
