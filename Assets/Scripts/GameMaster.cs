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
	}
		
	public Transform SavePoint;

	public static IEnumerator KillHuman(Human human) {
		yield return new WaitForSeconds(2);
		human.gameObject.transform.position = gm.SavePoint.position;
		GameObject.Find ("Alien").transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 2f, gm.SavePoint.position.z);
		human.health = 25;
		Image healthBar = GameObject.Find ("HumanHealthBarContent").GetComponent<Image> ();
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - human.health * 0.01f);
		healthBar.transform.localScale = new Vector3(human.healthScale.x * human.health * 0.01f, 1f, 1f);
	}

	public static IEnumerator KillAlien(Alien alien) {
		yield return new WaitForSeconds(2);
		GameObject.Find("Human").transform.position = gm.SavePoint.position;
		alien.gameObject.transform.position = new Vector3 (gm.SavePoint.position.x, gm.SavePoint.position.y + 2f, gm.SavePoint.position.z);
		alien.health = 25;
		Image healthBar = GameObject.Find ("AlienHealthBarContent").GetComponent<Image> ();
		healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - alien.health * 0.01f);
		healthBar.transform.localScale = new Vector3(alien.healthScale.x * alien.health * 0.01f, 1f, 1f);
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
