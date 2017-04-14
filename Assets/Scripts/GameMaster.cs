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


	void Awake() {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			SaveAndLoad.PlayerData data = (SaveAndLoad.PlayerData) bf.Deserialize (file);
			file.Close ();
			if (data.sceneId == SceneManager.GetActiveScene ().buildIndex) {
				GameObject.Find ("Human").transform.position = new Vector3 (data.SavePointX, data.SavePointY + 1f, data.SavePointZ);
				GameObject.Find ("Alien").transform.position = new Vector3 (data.SavePointX, data.SavePointY + 3f, data.SavePointZ);
			}
		}
	}

	void Start() {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameMaster>();
		}
		timer = gameObject.GetComponent<Timer> ();
		pausePanel = GameObject.Find ("GameMenu");
		pausePanel.SetActive(false);
		GameMaster.gm.ContinueGame ();
		Physics2D.gravity = new Vector2 (0, -9.8f);
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
		float totalSize = 0f;

		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject controlPanel = UICanvas.transform.Find ("ControlPanel").gameObject;
		if (controlPanel.transform.childCount > 0) {
			foreach (Transform child in controlPanel.transform) { 
				totalSize += child.GetComponent<RectTransform> ().sizeDelta.x + spacing;
			}
			placingX = -totalSize / 2f;
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

	public static void ShowTimerMessage(string message) {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject timerMessageText = UICanvas.transform.Find ("TimerText").gameObject;
		timerMessageText.GetComponent<Text> ().text = message;
	}

	public static void CloseTimerMessage() {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject timerMessageText = UICanvas.transform.Find ("TimerText").gameObject;
		timerMessageText.GetComponent<Text> ().text = "";
	}

	public static void ShowDialogMessage(string message) {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject dialogPanel = UICanvas.transform.Find ("DialogPanel").gameObject;
		GameObject controlPanel = UICanvas.transform.Find ("ControlPanel").gameObject;
		dialogPanel.SetActive (true);
		//if (!controlPanel.activeInHierarchy) {
		//	dialogPanel.SetActive (true);
		//}
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

		controlPanel.SetActive (true);

		//if (!dialogPanel.activeInHierarchy) {
		//	controlPanel.SetActive (true);
		//}
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
				//Debug.Log (textArray [i]);
				//Debug.Log (controlPanel.transform.position);
				//Debug.Log (GameMaster.gm.textPrefab.tag);
				GameObject textObject = Instantiate (GameMaster.gm.textPrefab, controlPanel.transform, false);
				textObject.GetComponent<Text> ().text = textArray [i];
				textObject.GetComponent<Text> ().color = Color.white;
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
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject gameMessageText = UICanvas.transform.Find ("GameMessageText").gameObject;
		GameObject blackCanvas = UICanvas.transform.Find ("BlackCanvas").gameObject;
		blackCanvas.GetComponent<FadeInOut> ().FadeOut ();
		gameMessageText.GetComponent<Text>().text = "You Died!!";
		yield return new WaitForSeconds (3f);

		human.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		human.GetComponent<Rigidbody2D> ().gravityScale = 0;

		human.gameObject.transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 3f, gm.SavePoint.position.z);
		if (human.attachedRope != null) {
			human.attachedRope.GetComponent<Rope> ().lastNode.GetComponent<HingeJoint2D> ().enabled = false;
		}
		human.gameObject.GetComponent<HumanMovements> ().attachedToRope = false;
		if (!human.gameObject.GetComponent<HumanMovements> ().m_FacingRight) {
			human.gameObject.GetComponent<HumanMovements> ().Flip ();
		}
		human.gameObject.transform.localScale = human.originalLocalScale;
		GameObject.Find ("Alien").transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 5f, gm.SavePoint.position.z);

		human.health = 50;
		human.humanDead = false;
		SpriteRenderer healthBar = GameObject.Find("HumanHealth").GetComponent<SpriteRenderer>();
		healthBar.transform.localScale = new Vector3(human.healthScale.x * human.health * 0.01f, 1f, 1f);

		if (gm.resetObjects.Length > 0) {
			for (int i = 0; i < gm.resetObjects.Length; i++) {
				if (gm.resetObjects [i].tag == "FallingIce") {
					gm.resetObjects [i].GetComponent<Falling> ().Reset ();
				}
				if (gm.resetObjects [i].tag == "GrowBall") {
					gm.resetObjects [i].GetComponent<GrowingBall> ().Reset ();
				}
				if (gm.resetObjects [i].tag == "IceFlower") {
					gm.resetObjects [i].GetComponent<ColliderReset> ().Reset ();
				}
					
			}
		}

		human.gameObject.GetComponent<Animator> ().SetBool ("UseRope", false);

		if (gm.timer != null && Timer.elevatorBrokenTimer == true) {
			gm.timer.remainingTime = 30f;
		}

		yield return new WaitForSeconds (2f);
		gameMessageText.GetComponent<Text>().text = "";
		blackCanvas.GetComponent<FadeInOut> ().FadeIn ();
		human.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		human.GetComponent<Rigidbody2D> ().gravityScale = 3;
	}

	public static IEnumerator KillAlien(Alien alien) {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject gameMessageText = UICanvas.transform.Find ("GameMessageText").gameObject;
		GameObject blackCanvas = UICanvas.transform.Find ("BlackCanvas").gameObject;
		blackCanvas.GetComponent<FadeInOut> ().FadeOut ();
		gameMessageText.GetComponent<Text>().text = "You Died!!";
		yield return new WaitForSeconds (3f);

		GameObject humanObject = GameObject.Find("Human");
		humanObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		humanObject.GetComponent<Rigidbody2D> ().gravityScale = 0;
		humanObject.transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 3f, gm.SavePoint.position.z);
		if (humanObject.GetComponent<Human> ().attachedRope != null) {
			humanObject.GetComponent<Human> ().attachedRope.GetComponent<Rope> ().lastNode.GetComponent<HingeJoint2D> ().enabled = false;
		}
		humanObject.GetComponent<HumanMovements> ().attachedToRope = false;
		if (!alien.gameObject.GetComponent<AlienMovements> ().m_FacingRight) {
			alien.gameObject.GetComponent<AlienMovements> ().Flip ();
		}
		alien.gameObject.transform.localScale = alien.originalLocalScale;
		alien.gameObject.transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 5f, gm.SavePoint.position.z);
		alien.health = 50;
		SpriteRenderer healthBar = GameObject.Find("AlienHealth").GetComponent<SpriteRenderer>();
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

		humanObject.GetComponent<Animator> ().SetBool ("UseRope", false);

		if (gm.timer != null && Timer.elevatorBrokenTimer == true) {
			gm.timer.remainingTime = 30f;
		}

		yield return new WaitForSeconds (2f);
		gameMessageText.GetComponent<Text>().text = "";
		blackCanvas.GetComponent<FadeInOut> ().FadeIn ();

		humanObject.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
		humanObject.GetComponent<Rigidbody2D> ().gravityScale = 3;
	}
}
