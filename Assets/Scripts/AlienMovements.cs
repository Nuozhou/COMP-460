using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovements : MonoBehaviour {

	[SerializeField] private float m_MaxSpeed = 10f;  
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;

	void Awake() {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	public void Move(float h, float v)
	{

		// Move the character
		m_Rigidbody2D.velocity = new Vector2(h*m_MaxSpeed, v*m_MaxSpeed);


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
