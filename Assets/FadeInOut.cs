using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FadeInOut : MonoBehaviour {
	public float fadeSpeed = 0.8f;
	private int fadeDir = -1;
	private RawImage image;

	void Start() {
		image = GetComponent<RawImage> ();
	}

	void Update() {
		
		if (fadeDir == -1 && image.color.a != 0) {
			float alpha = image.color.a + fadeDir * fadeSpeed * Time.deltaTime;
			image.color = new Color (image.color.r, image.color.g, image.color.b, Mathf.Clamp01 (alpha));
		} else if (fadeDir == 1 && image.color.a != 1) {
			float alpha = image.color.a + fadeDir * fadeSpeed * Time.deltaTime;
			image.color = new Color (image.color.r, image.color.g, image.color.b, Mathf.Clamp01 (alpha));
		}
	}

	public void FadeIn() {
		fadeDir = -1;
	}

	public void FadeOut() {
		fadeDir = 1;
	}

	void OnLevelFinishedLoading() {
		fadeDir = -1;
	}
}
