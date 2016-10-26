using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadTargetScene_buttoncontrol : MonoBehaviour {

	public void LoadSceneNum(int num){

		Scene scene = SceneManager.GetActiveScene();

		Debug.Log("Active scene is '" + scene.name + "'.");

		//LoadingScreenManager.LoadScene (num);
		SceneManager.LoadScene (num);

	}
}
