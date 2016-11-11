using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	public Light l1;
	public GameObject l3;
	public GameObject l2;
	public Vector3 offset;
    private Vector3 pumkinPos;
	public bool level2;
	private Vector3 v;
	private int i = 0;
	private Vector3 s;

	// Use this for initialization
	void Start () {
		offset = new Vector3(0, 60, -45);
		pumkinPos = Player.transform.position;
		transform.position = pumkinPos + offset;
		//offset = transform.position - pumkinPos;
		transform.LookAt(pumkinPos);
		v = l1.gameObject.transform.position;
		v.y = transform.position.y;
		v.x += 15f;
	}

	// Update is called once per frame
	void LateUpdate () {
		if (checkIsBlocked()) {
			//Debug.Log ("hit");
			//offset.z *= -1;
		}
		if (!level2) {
			pumkinPos = Player.transform.position;
			transform.position = pumkinPos + offset;
			transform.LookAt (pumkinPos);
			//Debug.Log (offset);
		} else {
			if (i ==0 ){
				s = transform.position;
				i = 1;
			}else if (i == 1) {
				transform.position = Vector3.MoveTowards (transform.position, v, 25f * Time.deltaTime);
				transform.LookAt (l3.transform.position);
				if ((Mathf.Abs (transform.position.x - v.x) < 0.1f) && (Mathf.Abs (transform.position.z - v.z) < 0.1f)) {
					i = 2;
				}
			} else if (i == 2) {
				l1.gameObject.SetActive (false);
				//transform.position = Vector3.MoveTowards (transform.position, l2.transform.position, 25f * Time.deltaTime);
				//transform.LookAt (l2.transform.position);
				//if ((Mathf.Abs (transform.position.x - l2.transform.position.x) < 0.1f) && (Mathf.Abs (transform.position.z - l2.transform.position.z) < 0.1f)) {
					i = 3;
				//}
			} else if (i == 3) {
				transform.position = Vector3.MoveTowards (transform.position, s, 25f * Time.deltaTime);
				level2 = false;
			}
				

		}

    }


	bool checkIsBlocked(){
		//return if a ray from camera to pumpkin hits something
		return Physics.Raycast (Player.transform.position, offset, 50f);
	}
}
