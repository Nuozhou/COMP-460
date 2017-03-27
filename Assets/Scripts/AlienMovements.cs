using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovements : MonoBehaviour {

	[SerializeField] private float m_MaxSpeed = 10f;
	private Rigidbody2D m_Rigidbody2D;
	private Camera camera;
	private float cameraHeight;
	private float cameraWidth;
	public bool m_FacingRight = true;
	private Animator m_Anim; 
	private Force force;
	private int waitTimer;

	void Awake() {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		m_Anim = GetComponent<Animator> ();
		camera = Camera.main;
		cameraHeight = camera.orthographicSize * 2f;
		cameraWidth = cameraHeight * camera.aspect;
		force = GetComponent<Force> ();
		waitTimer = 0;
	}

	void Update() {
		if (waitTimer > 35) {
			m_Anim.SetBool ("AlienWaiting", true);
		} else {
			m_Anim.SetBool ("AlienWaiting", false);
		}
	}

	public void Move(float h, float v)
	{
		if (h == 0f && v == 0f) {
			waitTimer++;
		} else {
			waitTimer = 0;
		}
		float newX = float.PositiveInfinity;
		float newY = float.PositiveInfinity;

		if (this.gameObject.transform.position.x >= camera.transform.position.x + cameraWidth / 2f - 0.5 && h > 0) {
			newX = 0f;
		} 
		if (this.gameObject.transform.position.x <= camera.transform.position.x - cameraWidth / 2f + 0.5 && h < 0) {
			newX = 0f;
		} 
		if (this.gameObject.transform.position.y >= camera.transform.position.y + cameraHeight / 2f - 0.5 && v > 0) {
			newY = 0f;
		} 
		if (this.gameObject.transform.position.y <= camera.transform.position.y - cameraHeight / 2f + 0.5 && v < 0) {
			newY = 0f;
		}

		if (newX == 0f && newY == 0f) {
			m_Rigidbody2D.velocity = new Vector2 (0f, 0f);
		} else if (newX == 0f && newY != 0f) {
			m_Rigidbody2D.velocity = new Vector2 (0f, v * m_MaxSpeed);
		} else if (newX != 0f && newY == 0f) {
			m_Rigidbody2D.velocity = new Vector2 (h * m_MaxSpeed, 0f);
		} else {
			m_Rigidbody2D.velocity = new Vector2 (h * m_MaxSpeed, v * m_MaxSpeed);
		}

		if (force.grabbedObject != null && force.grabbedObject.tag == "Grabbable") {
			Grabbable g = force.grabbedObject.GetComponent<Grabbable> ();
			Mathf.Clamp (transform.position.x, g.originalPosition.x - g.movableDistance - 5f, g.originalPosition.x + g.movableDistance - 5f);
			Mathf.Clamp (transform.position.y, g.originalPosition.y - g.movableDistance, g.originalPosition.y + g.movableDistance);

		}	

		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !m_FacingRight) {
			// ... flip the player.
			Flip ();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && m_FacingRight) {
			// ... flip the player.
			Flip ();
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

		GameObject.Find ("AlienHealthDisplay").transform.localScale = GameObject.Find ("AlienHealthDisplay").transform.localScale * (-1);
	}
}
