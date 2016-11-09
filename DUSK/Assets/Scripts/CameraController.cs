using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	private Vector3 offset;
    private Vector3 pumkinPos;

	// Use this for initialization
	void Start () {
		offset = new Vector3(0, 60, -45);
		pumkinPos = Player.transform.position;
		transform.position = pumkinPos + offset;
		//offset = transform.position - pumkinPos;
		transform.LookAt(pumkinPos);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (checkIsBlocked()) {
			Debug.Log ("hit");
			offset.z *= -1;
		}
		pumkinPos = Player.transform.position;
		transform.position = pumkinPos + offset;
		transform.LookAt(pumkinPos);
		Debug.Log (offset);

    }

	bool checkIsBlocked(){
		//return if a ray from camera to pumpkin hits something
		return Physics.Raycast (Player.transform.position, offset, 50f);
	}
}
