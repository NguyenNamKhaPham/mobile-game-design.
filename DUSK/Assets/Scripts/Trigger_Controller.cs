using UnityEngine;
using System.Collections;

public class Trigger_Controller : MonoBehaviour {

	public Light controlled_light;
	private Vector3 orig;
	private Vector3 location;
	private float speed;
	private bool is_triggered;

	// Use this for initialization
	void Start () {
		is_triggered = false;
		orig = transform.position;
		location = orig;
		location.x -= 3;
		speed = 10f;

	}

	public void Triggered(){
		if (is_triggered == false) {
			//gameObject.GetComponent<Renderer> ().material.color = Color.red;
			transform.position = location;
			if (controlled_light.intensity > 0) {
				controlled_light.gameObject.SetActive (false);
			}
			is_triggered = true;
		} else {
			//gameObject.GetComponent<Renderer> ().material.color = Color.green;
			transform.position = orig;
			controlled_light.gameObject.SetActive (true);
			is_triggered = false;
		}

	}
}
