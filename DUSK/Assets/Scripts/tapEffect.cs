using UnityEngine;
using System.Collections;

public class tapEffect : MonoBehaviour {
	public float speed = 1500f;
	public bool active = false;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (active) {
			transform.localScale = new Vector3 (6f, 6f, 6f);
			active = false;
		}
		transform.localScale = Vector3.MoveTowards (transform.localScale, new Vector3 (10f, 10f, 10f), speed*Time.deltaTime);
		if (transform.localScale == new Vector3 (10f, 10f, 10f)) {
			transform.localScale = new Vector3 (6f, 6f, 6f);
			this.gameObject.SetActive (false);
		}
	}
}
