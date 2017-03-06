using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameMaster : MonoBehaviour {

	public static GameMaster gm;
	public Timer timer;
	public Transform SavePoint;
	public Transform[] resetObjects;
	private GameObject pausePanel;

	public GameObject CrossPrefab;
	public GameObject CirclePrefab;
	public GameObject SquarePrefab;
	public GameObject TrianglePrefab;
	public GameObject L1Prefab;
	public GameObject L2Prefab;
	public GameObject R1Prefab;
	public GameObject R2Prefab;
	public GameObject LeftJoystickPrefab;
	public GameObject RightJoystickPrefab;
	public GameObject OptionPrefab;
	public GameObject textPrefab;

	[Serializable]
	class PlayerData
	{
		public int humanHealth;
		public int alienHealth;
		public int sceneId;
		public Transform SavePoint;
	}

	void Start() {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMaster>();
		}
		timer = gameObject.GetComponent<Timer> ();
		pausePanel = GameObject.Find ("GameMenu");
		pausePanel.SetActive(false);
		GameMaster.gm.ContinueGame ();
	}

	void Update() {
		if(Input.GetButtonDown ("GameMenu")) 
		{
			//Debug.Log ("Entered Game menu");
			if (!pausePanel.activeInHierarchy) 
			{
				PauseGame();
			}
			else if (pausePanel.activeInHierarchy) 
			{
				ContinueGame();   
			}
		} 
	}

	void LateUpdate() {
		float placingX = -230f;
		float spacing = 10f;

		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject controlPanel = UICanvas.transform.Find ("ControlPanel").gameObject;
		if (controlPanel.transform.childCount > 0) {
			foreach (Transform child in controlPanel.transform) {
				child.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (placingX + child.GetComponent<RectTransform> ().sizeDelta.x /2f, 0f);
				//Debug.Log ("sizeDelta: " + textObject.GetComponent<RectTransform> ().sizeDelta);
				//Debug.Log ("rectWidth: " + textObject.GetComponent<RectTransform> ().rect.width);
				placingX += child.GetComponent<RectTransform> ().sizeDelta.x + spacing;
			}
		} 
	}

	public void PauseGame()
	{
		//Debug.Log ("Entered pause");
		Time.timeScale = 0;
		pausePanel.SetActive(true);
		//Disable scripts that still work while timescale is set to 0
	} 

	public void ContinueGame()
	{
		//Debug.Log ("Exit pause");
		Time.timeScale = 1;
		pausePanel.SetActive(false);
		//enable the scripts again
	}

	public static void ShowDialogMessage(string message) {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject dialogPanel = UICanvas.transform.Find ("DialogPanel").gameObject;
		GameObject controlPanel = UICanvas.transform.Find ("ControlPanel").gameObject;
		if (!controlPanel.activeInHierarchy) {
			dialogPanel.SetActive (true);
		}
		GameObject dialogText = dialogPanel.transform.Find ("DialogText").gameObject;
		dialogText.GetComponent<Text> ().text = message;
	}

	public static void CloseDialogPanel() {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject dialogPanel = UICanvas.transform.Find ("DialogPanel").gameObject;
		dialogPanel.SetActive (false);
	}

	public static void ShowControlMessage(string message) {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject dialogPanel = UICanvas.transform.Find ("DialogPanel").gameObject;
		GameObject controlPanel = UICanvas.transform.Find ("ControlPanel").gameObject;
		foreach (Transform child in controlPanel.transform) {
			GameObject.Destroy(child.gameObject);
		}

		if (!dialogPanel.activeInHierarchy) {
			controlPanel.SetActive (true);
		}
		char[] delimiterChars = {' '};
		string[] textArray = message.Split (delimiterChars);

		for (int i = 0; i < textArray.Length; i++) {
			if (textArray [i] == "Cross") {
				GameObject cross = Instantiate (GameMaster.gm.CrossPrefab, controlPanel.transform, false);
			} else if (textArray [i] == "Circle") {
				GameObject cross = Instantiate (GameMaster.gm.CirclePrefab, controlPanel.transform, false);
			} else if (textArray [i] == "Square") {
				GameObject cross = Instantiate (GameMaster.gm.SquarePrefab, controlPanel.transform, false);
			} else if (textArray [i] == "Triangle") {
				GameObject cross = Instantiate (GameMaster.gm.TrianglePrefab, controlPanel.transform, false);
			} else if (textArray [i] == "R1") {
				GameObject cross = Instantiate (GameMaster.gm.R1Prefab, controlPanel.transform, false);
			} else if (textArray [i] == "L1") {
				GameObject cross = Instantiate (GameMaster.gm.L1Prefab, controlPanel.transform, false);
			} else if (textArray [i] == "R2") {
				GameObject cross = Instantiate (GameMaster.gm.R2Prefab, controlPanel.transform, false);
			} else if (textArray [i] == "L2") {
				GameObject cross = Instantiate (GameMaster.gm.L2Prefab, controlPanel.transform, false);
			} else if (textArray [i] == "LeftJoystick") {
				GameObject cross = Instantiate (GameMaster.gm.LeftJoystickPrefab, controlPanel.transform, false);
			} else if (textArray [i] == "RightJoystick") {
				GameObject cross = Instantiate (GameMaster.gm.RightJoystickPrefab, controlPanel.transform, false);
			} else if (textArray [i] == "Option") {
				GameObject cross = Instantiate (GameMaster.gm.OptionPrefab, controlPanel.transform, false);
			} else {
				GameObject textObject = Instantiate (GameMaster.gm.textPrefab, controlPanel.transform, false);
				textObject.GetComponent<Text> ().text = textArray [i];
			}

		}
	}

	public static void CloseControlPanel() {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject controlPanel = UICanvas.transform.Find ("ControlPanel").gameObject;
		controlPanel.SetActive (false);
		foreach (Transform child in controlPanel.transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

	public static IEnumerator KillHuman(Human human) {
		yield return new WaitForSeconds(2);
		human.gameObject.transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 1f, gm.SavePoint.position.z);
		if (GameObject.Find ("Human").GetComponent<Human> ().attachedRope != null) {
			GameObject.Find ("Human").GetComponent<Human> ().attachedRope.GetComponent<Rope> ().lastNode.GetComponent<HingeJoint2D> ().enabled = false;
		}
		GameObject.Find ("Human").GetComponent<HumanMovements> ().attachedToRope = false;
		GameObject.Find ("Alien").transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 3f, gm.SavePoint.position.z);
		human.health = 50;
		SpriteRenderer healthBar = GameObject.Find("HumanHealth").GetComponent<SpriteRenderer>();
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - human.health * 0.01f);
		healthBar.transform.localScale = new Vector3(human.healthScale.x * human.health * 0.01f, 1f, 1f);

		if (gm.resetObjects.Length > 0) {
			for (int i = 0; i < gm.resetObjects.Length; i++) {
				if (gm.resetObjects [i].tag == "FallingIce") {
					gm.resetObjects [i].GetComponent<Falling> ().Reset ();
				}
				if (gm.resetObjects [i].tag == "GrowBall") {
					gm.resetObjects [i].GetComponent<GrowingBall> ().Reset ();
				}
			}
		}


		if (gm.timer != null && Timer.elevatorBrokenTimer == true) {
			gm.timer.remainingTime = 30f;
		}
	}

	public static IEnumerator KillAlien(Alien alien) {
		yield return new WaitForSeconds(2);
		GameObject.Find("Human").transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 1f, gm.SavePoint.position.z);
		if (GameObject.Find ("Human").GetComponent<Human> ().attachedRope != null) {
			GameObject.Find ("Human").GetComponent<Human> ().attachedRope.GetComponent<Rope> ().lastNode.GetComponent<HingeJoint2D> ().enabled = false;
		}
		GameObject.Find ("Human").GetComponent<HumanMovements> ().attachedToRope = false;
		alien.gameObject.transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 3f, gm.SavePoint.position.z);
		alien.health = 50;
		SpriteRenderer healthBar = GameObject.Find("AlienHealth").GetComponent<SpriteRenderer>();
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - alien.health * 0.01f);
		healthBar.transform.localScale = new Vector3(alien.healthScale.x * alien.health * 0.01f, 1f, 1f);

		if (gm.resetObjects.Length > 0) {
			for (int i = 0; i < gm.resetObjects.Length; i++) {
				if (gm.resetObjects [i].tag == "FallingIce") {
					gm.resetObjects [i].GetComponent<Falling> ().Reset ();
				}
				if (gm.resetObjects [i].tag == "GrowBall") {
					gm.resetObjects [i].GetComponent<GrowingBall> ().Reset ();
				}
			}
		}

		if (gm.timer != null && Timer.elevatorBrokenTimer == true) {
			gm.timer.remainingTime = 30f;
		}
	}

	public static void Save() {
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		data.humanHealth = GameObject.Find ("Human").GetComponent<Human> ().health;
		data.alienHealth = GameObject.Find ("Alien").GetComponent<Alien> ().health;
		data.sceneId = SceneManager.GetActiveScene ().buildIndex;
		data.SavePoint = gm.SavePoint;
		bf.Serialize (file, data);
		file.Close ();

	}

	public static void Load() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize (file);
			SceneManager.LoadScene (data.sceneId);
			file.Close ();
		}
	}
}
