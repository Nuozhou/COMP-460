using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveAndLoad : MonoBehaviour {

	public float savePointX;
	public float savePointY;
	public float savePointZ;

	[Serializable]
	public class PlayerData
	{
		public int sceneId;
		public float SavePointX;
		public float SavePointY;
		public float SavePointZ;

	}

	public void Save() {
		/*
		Debug.Log ("Save called");
		Debug.Log ("sceneID: " + SceneManager.GetActiveScene ().buildIndex);
		PlayerPrefs.SetInt ("sceneID", SceneManager.GetActiveScene ().buildIndex);
		Debug.Log ("PlayerprefsID: " + PlayerPrefs.GetInt ("sceneID"));
		PlayerPrefs.SetFloat("savePointX", GameMaster.gm.SavePoint.position.x);
		PlayerPrefs.SetFloat("savePointY", GameMaster.gm.SavePoint.position.y);
		PlayerPrefs.SetFloat("savePointZ", GameMaster.gm.SavePoint.position.z);
		Debug.Log ("savePointX: " + PlayerPrefs.GetFloat ("savePointX"));
		Debug.Log ("savePointY: " + PlayerPrefs.GetFloat ("savePointY"));
		Debug.Log ("savePointZ: " + PlayerPrefs.GetFloat ("savePointZ"));
		PlayerPrefs.Save ();
		*/

		Debug.Log ("Save called");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData ();
		data.sceneId = SceneManager.GetActiveScene ().buildIndex;
		data.SavePointX = GameMaster.gm.SavePoint.position.x;
		data.SavePointY = GameMaster.gm.SavePoint.position.y;
		data.SavePointZ = GameMaster.gm.SavePoint.position.z;
		bf.Serialize (file, data);
		file.Close ();


	}

	public void Load() {
		/*
		Debug.Log ("Load called");
		int sceneID = PlayerPrefs.GetInt ("sceneID");
		Debug.Log ("PlayerprefsID: " + PlayerPrefs.GetInt ("sceneID"));
		Debug.Log ("savePointX: " + PlayerPrefs.GetFloat ("savePointX"));
		Debug.Log ("savePointY: " + PlayerPrefs.GetFloat ("savePointY"));
		Debug.Log ("savePointZ: " + PlayerPrefs.GetFloat ("savePointZ"));
		SceneManager.LoadScene (sceneID);
		*/
		Debug.Log ("Load called");
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			Debug.Log ("Load entered");
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize (file);
			file.Close ();
			savePointX = data.SavePointX;
			savePointY = data.SavePointY;
			savePointZ = data.SavePointZ;
			SceneManager.LoadScene (data.sceneId);
		}

	}


}
