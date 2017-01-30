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

	void Awake() {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		camera = Camera.main;
		cameraHeight = camera.orthographicSize * 2f;
		cameraWidth = cameraHeight * camera.aspect;
	}

	public void Move(float h, float v)
	{

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
			// Move the character
			m_Rigidbody2D.velocity = new Vector2(h * m_MaxSpeed, v * m_MaxSpeed);
		}

		// If the input is moving the player right and the player is facing left...
		if (h > 0 && !m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (h < 0 && m_FacingRight)
		{
			// ... flip the player.
			Flip();
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
