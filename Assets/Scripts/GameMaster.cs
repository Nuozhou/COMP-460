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
	public Sprite CrossButtonImage;
	public Sprite CircleButtonImage;
	public Sprite SquareButtonImage;
	public Sprite TriangleButtonImage;
	public Sprite L1ButtonImage;
	public Sprite L2ButtonImage;
	public Sprite R1ButtonImage;
	public Sprite R2ButtonImage;
	public Sprite LeftJoystick;
	public Sprite RightJoystick;


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
		StartCoroutine(ShowButtonMessage ("Use Left Joystick ", " to move", "LeftJoystick"));
	}

	void Update() {
		if(Input.GetButtonDown ("GameMenu")) 
		{
			Debug.Log ("Entered Game menu");
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

	public void PauseGame()
	{
		Debug.Log ("Entered pause");
		Time.timeScale = 0;
		pausePanel.SetActive(true);
		//Disable scripts that still work while timescale is set to 0
	} 

	public void ContinueGame()
	{
		Debug.Log ("Exit pause");
		Time.timeScale = 1;
		pausePanel.SetActive(false);
		//enable the scripts again
	}

	public static IEnumerator ShowButtonMessage(string message1, string message2, string buttonName) {
		GameObject UICanvas = GameObject.Find ("UICanvas");
		GameObject message1UI = UICanvas.transform.Find ("ButtonMessageText1").gameObject;
		GameObject buttonUI = UICanvas.transform.Find ("ButtonImage").gameObject;
		GameObject message2UI = UICanvas.transform.Find ("ButtonMessageText2").gameObject;
		if (buttonName == "Cross") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.CrossButtonImage;
		} else if (buttonName == "Circle") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.CircleButtonImage;
		} else if (buttonName == "Square") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.SquareButtonImage;
		} else if (buttonName == "Triangle") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.TriangleButtonImage;
		} else if (buttonName == "R1") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.R1ButtonImage;
		} else if (buttonName == "L1") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.L1ButtonImage;
		} else if (buttonName == "R2") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.R2ButtonImage;
		} else if (buttonName == "L2") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.L2ButtonImage;
		} else if (buttonName == "LeftJoystick") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.LeftJoystick;
		} else if (buttonName == "RightJoystick") {
			buttonUI.GetComponent<Image> ().sprite = GameMaster.gm.RightJoystick;
		}

		message1UI.GetComponent<Text> ().text = message1;
		message2UI.GetComponent<Text> ().text = message2;
 
		message1UI.SetActive (true);
		buttonUI.SetActive (true);
		message2UI.SetActive (true);
		yield return new WaitForSeconds(3f);
		message1UI.SetActive (false);
		buttonUI.SetActive (false);
		message2UI.SetActive (false);
	}

	public static IEnumerator KillHuman(Human human) {
		yield return new WaitForSeconds(2);
		human.gameObject.transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 1f, gm.SavePoint.position.z);
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
			}
		}


		if (gm.timer != null && Timer.elevatorBrokenTimer == true) {
			gm.timer.remainingTime = 30f;
		}
	}

	public static IEnumerator KillAlien(Alien alien) {
		yield return new WaitForSeconds(2);
		GameObject.Find("Human").transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 1f, gm.SavePoint.position.z);
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
