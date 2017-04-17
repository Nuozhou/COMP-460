using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class HumanMovements : MonoBehaviour {

	[SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
	[SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

	public AudioClip jumpStartClip;
	public AudioClip jumpEndClip;
	public AudioClip ropeSwingClip;
	private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
	const float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	public bool standingOnAlien;
	public bool attachedToRope;
	private Transform m_CeilingCheck;   // A position marking where to check for ceilings
	const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
	private Animator m_Anim;            // Reference to the player's animator component.
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Camera camera;
	private float cameraHeight;
	private float cameraWidth;
	private bool jumped = false;

	private void Awake()
	{
		// Setting up references.
		m_GroundCheck = transform.Find("GroundCheck");
		m_CeilingCheck = transform.Find("CeilingCheck");
		m_Anim = GetComponent<Animator>();
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		camera = Camera.main;
		attachedToRope = false;
		cameraHeight = camera.orthographicSize * 2f;
		cameraWidth = cameraHeight * camera.aspect;
	}

	private void Update() {

		bool operate = Input.GetButtonDown ("Operate");
		if (operate && SceneManager.GetActiveScene().buildIndex == 5) {
			if (Physics2D.gravity.y < 0f) {
				Physics2D.gravity = new Vector2 (0, 2f);
				m_Anim.SetBool ("Floating", true);
			} else {
				Physics2D.gravity = new Vector2 (0, -9.8f);
				m_Anim.SetBool ("Floating", false);
			}
		}

		//Debug.Log ("Floating: " + m_Anim.GetBool ("Floating"));
		/*
		Debug.Log ("grounded: " + m_Grounded);
		Debug.Log ("Jumped:" + jumped);
		if (jumped && m_Grounded) {
			GetComponent<AudioSource> ().clip = jumpEndClip;
			GetComponent<AudioSource> ().Play ();
			Debug.Log ("played jump end sound");
			jumped = false;
		}
		*/
		/*
		if (jumped && m_Grounded) {
			Debug.Log ("played jump end sound");
			GetComponent<AudioSource> ().clip = jumpEndClip;
			GetComponent<AudioSource> ().Play ();
			jumped = false;
		}
		*/
	}


	private void FixedUpdate()
	{
		m_Grounded = false;
		standingOnAlien = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders [i].gameObject.name == "Alien") {
				standingOnAlien = true;
				StartCoroutine (waitHumanJump ());
			} 

			if (colliders [i].gameObject != gameObject) {
				m_Grounded = true;
			}
		}
			
		m_Anim.SetBool("Ground", m_Grounded);

		// Set the vertical animation
		m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
	}

	private IEnumerator waitHumanJump() {
		yield return new WaitForSeconds(1f);
		if (standingOnAlien) {
			standingOnAlien = false;
		}
		GameObject.Find ("Alien").GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds(3f);
		GameObject.Find ("Alien").GetComponent<BoxCollider2D> ().enabled = true;


	}


	public void Move(float move, bool crouch, bool jump, bool triangle)
	{
		// If crouching, check to see if the character can stand up
		/*
		if (!crouch && m_Anim.GetBool("Crouch"))
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				Debug.Log("Executed");
				Debug.Log (m_CeilingCheck.position);
				Debug.Log (k_CeilingRadius);
				crouch = true;
			}
		}
		*/

		// Set whether or not the character is crouching in the animator
		m_Anim.SetBool("Crouch", crouch);

		if (attachedToRope) {
			//Debug.Log ("Rope move");
			m_Anim.SetFloat ("Speed", Mathf.Abs (move));
			m_Rigidbody2D.AddForce (new Vector2 (1.5f * move * m_MaxSpeed, 0f));
			if (!GetComponent<AudioSource> ().isPlaying && m_Rigidbody2D.velocity.x > 9f) {
				GetComponent<AudioSource> ().clip = ropeSwingClip;
				GetComponent<AudioSource> ().Play ();
			}

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight) {
				// ... flip the player.
				Flip ();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight) {
				// ... flip the player.
				Flip ();
			}
			return;
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Reduce the speed if crouching by the crouchSpeed multiplier
			move = (crouch ? move*m_CrouchSpeed : move);

			// The Speed animator parameter is set to the absolute value of the horizontal input.
			m_Anim.SetFloat("Speed", Mathf.Abs(move));

			if (this.gameObject.transform.position.x >= camera.transform.position.x + cameraWidth / 2f - 0.34 && m_FacingRight) {
				m_Rigidbody2D.velocity = new Vector2 (0, m_Rigidbody2D.velocity.y);
			}
			else if (this.gameObject.transform.position.x <= camera.transform.position.x - cameraWidth / 2f + 0.34 && !m_FacingRight) {
				m_Rigidbody2D.velocity = new Vector2 (0, m_Rigidbody2D.velocity.y);
			} else {
				// Move the character
				m_Rigidbody2D.velocity = new Vector2 (move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
			}

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump && m_Anim.GetBool("Ground"))
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			jumped = true;
			m_Anim.SetBool("Ground", false);
			GetComponent<AudioSource> ().clip = jumpStartClip;
			GetComponent<AudioSource> ().Play ();
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
			if (standingOnAlien) {
				standingOnAlien = false;
			}
		}


		if (Physics2D.gravity.y >= -2f && jump) {
			m_Rigidbody2D.AddForce(new Vector2(0f, -0.2f * m_JumpForce));
		}

		if (Physics2D.gravity.y >= -2f && triangle) {
			m_Rigidbody2D.AddForce(new Vector2(0f, 0.2f * m_JumpForce));
		}

	}


	public void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

		GameObject.Find ("HumanHealthDisplay").transform.localScale = GameObject.Find ("HumanHealthDisplay").transform.localScale * (-1);
	}
}
