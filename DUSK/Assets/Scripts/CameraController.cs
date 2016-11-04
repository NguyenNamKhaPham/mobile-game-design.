using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	private Vector3 offset;
    private Vector3 position;

	// Use this for initialization
	void Start () {
		offset = new Vector3(0, 60, -45);
        position = Player.transform.position;
        transform.position = position + offset;
		offset = transform.position - position;
        transform.LookAt(position);
	}
	
	// Update is called once per frame
	void LateUpdate () {
        position = Player.transform.position;
        transform.position = position + offset;
        transform.LookAt(position);
    }
}
