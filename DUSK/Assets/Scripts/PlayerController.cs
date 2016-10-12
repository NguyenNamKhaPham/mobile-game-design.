using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	

	public float speed;
	public Text infotext;

	private ShadowDetector sd;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;

		sd = transform.GetComponent<ShadowDetector>();

	}
	
	// Update is called once per frame
	void Update () {
		if (sd.isShaded) {
			infotext.text = "DUSK";
			infotext.color = Color.black;
		} else {
			infotext.text = "LIGHT";
			infotext.color = Color.red;
		}

	}

	void FixedUpdate () {

		Vector3 pos = Input.mousePosition;
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);


		rb.velocity = movement * speed;


	}
}
