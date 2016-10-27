using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public float speedx;
	public float speedy;
	public float speedz;
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (speedx, speedy, speedz) * Time.deltaTime);
	}
}
