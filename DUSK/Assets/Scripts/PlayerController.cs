using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	

	public float speed;
	public Text indark;
	public Text candycount;
	public Text ExitWarning;

	private ShadowDetector sd;
	private Rigidbody rb;
	private int candynum;

	public Canvas ending_screen;
	public Button pause_button;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
		sd = transform.GetComponent<ShadowDetector>();

		SetCountText ();
		candynum = 0;

		//rb.transform.position = new Vector3 (175,0.6,45);
		ExitWarning.enabled = false;

		if (ending_screen.gameObject.activeInHierarchy == true) {
			ending_screen.gameObject.SetActive (false);
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (sd.isShaded) {
			indark.text = "DUSK";
			indark.color = Color.black;
		} else {
			indark.text = "LIGHT";
			indark.color = Color.red;
			StartCoroutine (ShowMessage (ExitWarning, "You Shall Not Embrace the Light", 4));
			rb.transform.position = new Vector3(175, 1, 45);
		}
	}

	void FixedUpdate () {
		
		//Vector3 pos = Input.mousePosition;
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		if (Input.touchCount > 0) {
			// The screen has been touched so store the touch
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				// If the finger is on the screen, move the object smoothly to the touch position
				moveHorizontal = (touch.position.x - Screen.width/2)/(Screen.width/2);
				moveVertical = (touch.position.y - Screen.height/2)/(Screen.height/2);
				movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			}
			Debug.Log (movement);
		}

		rb.velocity = movement * speed;
	}


	void OnTriggerEnter(Collider other){
		//Candy collector
		if (other.gameObject.CompareTag ("Candy")) {
			other.gameObject.SetActive (false);
			candynum += 1;
			SetCountText ();
		}

		//Exit Level
		if (other.gameObject.CompareTag ("Exittrigger")) {
			
			if (candynum > 2) {
				Time.timeScale = 0;
				if (ending_screen.gameObject.activeInHierarchy == false) {
					ending_screen.gameObject.SetActive (true);
					pause_button.gameObject.SetActive (false);
				}

			} else {
				StartCoroutine (ShowMessage (ExitWarning, "Need More Candies For Gate Opening", 3));

			}
		}
	}

	void SetCountText(){
		candycount.text = "Candy " + candynum.ToString () + "/6";
	}

	IEnumerator ShowMessage (Text guiText, string message, float delay) {
		guiText.text = message;
		guiText.enabled = true;
		yield return new WaitForSeconds(delay);
		guiText.enabled = false;
	}


}
