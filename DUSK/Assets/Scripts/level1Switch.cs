using UnityEngine;
using System.Collections;

public class level1Switch : MonoBehaviour {

	public Light controlled_light;
	// Use this for initialization
	void Start () {
	}

	public void Triggered(){
		this.gameObject.SetActive (false);
		controlled_light.gameObject.SetActive (false);
	}
}