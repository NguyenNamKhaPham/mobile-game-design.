using UnityEngine;
using System.Collections;

public class Trigger_Controller : MonoBehaviour {

	//public Light controlled_light;
	public GameObject camera;
	private bool is_triggered;

	// Use this for initialization
	void Start () {
		is_triggered = false;
	}

	public void Triggered(){
		if (is_triggered == false) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
			camera.GetComponent<CameraController> ().level2 = true;
		}
		is_triggered = true;
	}
		



}
