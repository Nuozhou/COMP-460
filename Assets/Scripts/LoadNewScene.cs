using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadNewScene : MonoBehaviour {

	public void LoadByIndex(int sceneIndex) {
		SceneManager.LoadScene (sceneIndex);
	}

	public void LoadResetByIndex(int sceneIndex) {
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			File.Delete (Application.persistentDataPath + "/playerInfo.dat");
		}
		SceneManager.LoadScene (sceneIndex);
	}

	public void LoadMainMenu() {
		if (SceneManager.GetActiveScene ().buildIndex != 2) {
			GameMaster.gm.ContinueGame ();
		}
		SceneManager.LoadScene (0);
	}
}
