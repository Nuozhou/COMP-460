﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D col) {
		if(col.gameObject.tag == "Enemy")
		{
			// ... find the Enemy script and call the Hurt function.
			col.gameObject.GetComponent<Enemy>().Damage(100);
		}
	}

}
