using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	

	public float speed;
	public Text indark;
	public Text candycount;
	public Text ExitWarning;

	private GameObject wagon1;
	private GameObject wagon2;
	private Vector3 moveVector;
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
		wagon1 = GameObject.FindGameObjectWithTag ("cart1");
		wagon2 = GameObject.FindGameObjectWithTag ("cart2");
		speed = 10f;
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
			wagon1.transform.position = new Vector3 (181f, 0f, 138f);
			wagon2.transform.position = new Vector3 (158f, 0f, 255f);
		}
	}

	void FixedUpdate () {
		
		//use keyboard to move
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//use touch screen to move
		if (Input.touchCount > 0) {
			// The screen has been touched so store the touch
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				// If the finger is on the screen, move the object smoothly to the touch position
				moveHorizontal = (touch.position.x - Screen.width/2)/(Screen.width/2);
				moveVertical = (touch.position.y - Screen.height/2)/(Screen.height/2);
			}
		}
		moveVector = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		Vector3 faceTo = new Vector3 (-moveHorizontal, 0.0f, -moveVertical);
		if (moveVector != Vector3.zero)
			transform.rotation = Quaternion.LookRotation(faceTo);
		rb.velocity =  moveVector * speed;
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
