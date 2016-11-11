using UnityEngine;
using System.Collections;

public class level1Switch : MonoBehaviour {

	public Light controlled_light;
	private bool is_triggered;

	// Use this for initialization
	void Start () {
		is_triggered = false;
	}

	public void Triggered(){
		if (!is_triggered) {
			//gameObject.GetComponent<Renderer> ().material.color = Color.red;
			if (controlled_light.intensity > 0) {
				controlled_light.gameObject.SetActive (false);
				Debug.Log("111111");
			}

			is_triggered = !is_triggered;
		} else {
			//gameObject.GetComponent<Renderer> ().material.color = Color.green;
			controlled_light.gameObject.SetActive (true);
			is_triggered = !is_triggered;
		}

	}
}