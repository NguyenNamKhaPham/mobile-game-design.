using UnityEngine;
using System.Collections;

public class clipping_controller : MonoBehaviour {

	public Vector3 clipping_postion;
	public Vector3 clipping_scale;



	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("movableObject")) {
			other.gameObject.transform.position = clipping_postion;
			other.gameObject.transform.localScale = clipping_scale;
		}
	}
}
