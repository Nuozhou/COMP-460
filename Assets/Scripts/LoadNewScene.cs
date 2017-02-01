using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour {

	public void LoadByIndex(int sceneIndex) {
		SceneManager.LoadScene (sceneIndex);
	}

	public void LoadMainMenu() {
		GameMaster.gm.ContinueGame ();
		SceneManager.LoadScene (0);
	}
}
