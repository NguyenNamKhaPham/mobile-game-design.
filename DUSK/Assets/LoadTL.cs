using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class LoadTL : MonoBehaviour {

	public void LoadSceneNum(int num){

		Scene scene = SceneManager.GetActiveScene();

		Debug.Log("Active scene is '" + scene.name + "'.");

		LoadingScreenManager.LoadScene (num);
		//sSceneManager.LoadScene (num);

	}
}
