using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGeneration : MonoBehaviour {

	public Transform starPrefab;

	void Start() {
		InvokeRepeating ("generateRandomStar", 0f, 2f);
	}

	void generateRandomStar() {
		Vector3 screenPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,Screen.width), Random.Range(0,Screen.height), Camera.main.farClipPlane/2));
		GameObject star = Instantiate (starPrefab.gameObject, new Vector3(screenPosition.x, screenPosition.y, 190f), Quaternion.identity);
		star.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
		GameMaster.gm.GetComponent<Parallaxing> ().backgroundObjects.Add (star.transform);
		int count = GameMaster.gm.GetComponent<Parallaxing> ().backgroundObjects.Count;
		GameMaster.gm.GetComponent<Parallaxing> ().scales.Add (GameMaster.gm.GetComponent<Parallaxing> ().backgroundObjects [count - 1].position.z * (-1));
		StartCoroutine (deleteStar (star.transform));
	}

	IEnumerator deleteStar(Transform star) {
		yield return new WaitForSeconds (6f);
		int index = GameMaster.gm.GetComponent<Parallaxing> ().backgroundObjects.IndexOf (star);
		GameMaster.gm.GetComponent<Parallaxing> ().backgroundObjects.RemoveAt (index);
		GameMaster.gm.GetComponent<Parallaxing> ().scales.RemoveAt (index);
		Destroy (star.gameObject);
	}
}
