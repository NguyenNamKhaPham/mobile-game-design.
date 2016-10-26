using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu_Controller : MonoBehaviour {
	public GameObject player;
	public Canvas pause_canvas;
	public Button pause_button;

	void Start () {
		if (pause_canvas.gameObject.activeInHierarchy == true) {
			pause_canvas.gameObject.SetActive (false);
		}
	}

	public void restart_level() {
		player.transform.position = new Vector3(175, 1, 45);
		if (pause_canvas.gameObject.activeInHierarchy == true) {
			pause_canvas.gameObject.SetActive (false);
			pause_button.gameObject.SetActive (true);
			Time.timeScale = 1;
		}
	}


	public void pause_pressed(){
		if (pause_canvas.gameObject.activeInHierarchy == false) {
			pause_canvas.gameObject.SetActive (true);
			pause_button.gameObject.SetActive (false);
			Time.timeScale = 0;
		}
	}

	public void resume_pressed(){
		if (pause_canvas.gameObject.activeInHierarchy == true) {
			pause_canvas.gameObject.SetActive (false);
			pause_button.gameObject.SetActive (true);
			Time.timeScale = 1;
		}
	}

}