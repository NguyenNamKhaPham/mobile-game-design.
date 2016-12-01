using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Start_Menu_Camera_Controller : MonoBehaviour {
	public GameObject currentmount; 
	public float speed;

	void Start () {
		transform.position = new Vector3 (-150,50,-5);
	}
	void Update () {
		transform.position = Vector3.Lerp (transform.position, currentmount.transform.position, speed);
		transform.rotation = Quaternion.Slerp(transform.rotation, currentmount.transform.rotation, speed);
	}

	public void SetMount(GameObject newmount){
		currentmount = newmount;
	}

}
