using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public Text candycount;
	public Text ExitWarning;
	public Canvas ending_screen;
	public Button pause_button;
	public int candytotal;
	public Vector3 original_pos;
	public int required_candynum;
	private int candynum;

	//for shadow detection
	private ShadowDetector sd;

	//For movement
	private GameObject tap;
	private Vector3 tapLocation;
	private Ray ray;
	private Rigidbody rb;
	public float speed;

	//for movable objects
	GameObject[] movedObjects;
	Vector3[] movedOjectsPosition;

	// Use this for initialization
	void Start () {
		//for movement
		tap = GameObject.Find ("tapEffect");
		tapLocation = transform.position;
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
		speed = 2f;

		//for detect shadow
		sd = transform.GetComponent<ShadowDetector>();

		//set candy
		SetCountText ();
		candynum = 0;

		//store all movable object positions
		movedObjects = GameObject.FindGameObjectsWithTag("movableObject");
		movedOjectsPosition = new Vector3[movedObjects.Length];
		for (int i = 0; i < movedObjects.Length; i++) {
			movedOjectsPosition [i] = movedObjects [i].transform.position;
		}
			
		ExitWarning.enabled = false;

		if (ending_screen.gameObject.activeInHierarchy == true) {
			ending_screen.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//hit light
		if (!sd.isShaded) {
			//pop up warning, pumpkin stops and resqpawns, movable objects respawn
			StartCoroutine (ShowMessage (ExitWarning, "You Shall Not Embrace the Light", 4));
			transform.position = original_pos;
			tapLocation = original_pos;
			rb.velocity = Vector3.zero;
			for (int i = 0; i < movedObjects.Length; i++) {
				movedObjects [i].transform.position = movedOjectsPosition [i];
			}
		}
	}

	void FixedUpdate () {
		//mouse click or touch, get ray
		if (Input.GetMouseButton (0) || Input.touchCount > 0) {
			if (Input.GetMouseButton (0)) {
				ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			} else {
				ray = Camera.main.ScreenPointToRay (Input.touches [0].position);
			}
			//identify hit, find the correct hit
			RaycastHit[] hits;
			hits = Physics.RaycastAll (ray);
			for (int i = 0; i < hits.Length; i++) {
				RaycastHit hit = hits [i];
				if (hit.collider.name == "floor") {
					indicateTap (hit);
					break;
				}
			}
		}
		//get to that location
		if ((Mathf.Abs (transform.position.x - tapLocation.x) > 0.1f) || (Mathf.Abs (transform.position.z - tapLocation.z) > 0.1f)) {
			rb.velocity = (tapLocation - transform.position) * speed;
		} else {
			tap.SetActive (false);
		}
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
			
			if (candynum >= required_candynum) {
				Time.timeScale = 0;
				if (ending_screen.gameObject.activeInHierarchy == false) {
					ending_screen.gameObject.SetActive (true);
					pause_button.gameObject.SetActive (false);
				}

			} else {
				StartCoroutine (ShowMessage (ExitWarning, "Need More Candies For Gate Opening", 3));
			}
		}

		if (other.gameObject.CompareTag ("Button_trigger")) {
			Trigger_Controller Trigger_Controller_script = other.GetComponent<Trigger_Controller>();
			Trigger_Controller_script.Triggered();
		}

	}

	void SetCountText(){
		candycount.text = "Candy " + candynum.ToString () + "/" + candytotal;
	}

	//indicate successful tap/click, store and face to that location
	void indicateTap(RaycastHit hit){
		tapLocation = hit.point;
		tapLocation.y = transform.position.y;
		tap.GetComponent <tapEffect> ().active = true;
		tap.SetActive (true);
		tap.transform.position = tapLocation;
		transform.LookAt (tapLocation);
	}

	IEnumerator ShowMessage (Text guiText, string message, float delay) {
		guiText.text = message;
		guiText.enabled = true;
		yield return new WaitForSeconds(delay);
		guiText.enabled = false;
	}


}
