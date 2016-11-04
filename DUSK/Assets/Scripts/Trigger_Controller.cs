using UnityEngine;
using System.Collections;

public class Trigger_Controller : MonoBehaviour {

	public Light controlled_light;

	private bool is_triggered;

	// Use this for initialization
	void Start () {
		is_triggered = false;
	}

	public void Triggered(){
		if (is_triggered == false) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
			if (controlled_light.intensity > 0) {
				controlled_light.gameObject.SetActive(false);
			}
		}
		is_triggered = true;
	}



}
