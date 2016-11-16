using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	
	public Text candycount;
	public Text ExitWarning;
	public Canvas ending_screen;
	public Canvas death_canvas;
	public Button pause_button;
	public int candytotal;
	public Vector3 original_pos;
	public int required_candynum;
	private int candynum;

	public bool keys;
	public bool test_mode;
	private bool notActivate = true;
	private Animator anim;

	//for shadow detection
	private ShadowDetector sd;

	//For movement
	public GameObject tap;
	private Vector3 tapLocation;
	private Ray ray; 
	private Rigidbody rb;
	public float speed;
	private Vector3 d;

	//for movable objects
	GameObject[] movedObjects;
	Vector3[] movedOjectsPosition;

	// Use this for initialization
	void Start () {
		//store original position
		original_pos = transform.position;

		//shadow detector
		sd = this.gameObject.GetComponent<ShadowDetector>();

		//for movement
		tapLocation = transform.position;
		rb = GetComponent<Rigidbody> ();
		rb.freezeRotation = true;

		//store all movable object positions
		movedObjects = GameObject.FindGameObjectsWithTag("movableObject");
		movedOjectsPosition = new Vector3[movedObjects.Length];
		for (int j = 0; j < movedObjects.Length; j++) {
			movedOjectsPosition [j] = movedObjects [j].transform.position;
		}

		anim = GameObject.FindGameObjectWithTag("pumpkin").GetComponent<Animator>();

		ExitWarning.enabled = false;

		if (ending_screen.gameObject.activeInHierarchy == true) {
			ending_screen.gameObject.SetActive (false);
		}

		//set candy
		SetCountText ();
		candynum = 0;


	}

	void Update(){
		//hit light
		if (!sd.isShaded) {
			//pop up warning, pumpkin stops and resqpawns, movable objects respawn
			if(test_mode){
				StartCoroutine (ShowMessage (ExitWarning, "You Shall Not Embrace the Light", 4));
				keys = false;
				rb.velocity = Vector3.zero;
				anim.SetBool ("isDead", true);
				StartCoroutine (respawn ());   
			} else if (death_canvas.gameObject.activeInHierarchy == false) {
				death_canvas.gameObject.SetActive (true);
				pause_button.gameObject.SetActive (false);
				Time.timeScale = 0;
			}



		} else {
			//Debug.Log ("live");
		}
	}


	void FixedUpdate () {

		if (keys) {
			if ((Input.GetMouseButton (0) || Input.touchCount == 1)) {
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
			d = (tapLocation - transform.position).normalized;
		}

		if ((Mathf.Abs (transform.position.x - tapLocation.x) < 0.5f) && (Mathf.Abs (transform.position.z - tapLocation.z) < 0.5f)) {
			d = Vector3.zero;
		}
		rb.MovePosition (transform.position + d * Time.deltaTime * speed);
	}


	void OnTriggerEnter(Collider other){
		//Candy collector
		if (other.gameObject.CompareTag ("Candy")) {
			other.gameObject.SetActive (false);
			candynum += 1;
			SetCountText ();
			keys = false;
			tapLocation = transform.position;
			rb.velocity = Vector3.zero;
			Camera.main.gameObject.GetComponent<CameraController> ().unlockALock = true;
		}

		//Exit Level
		else if (other.gameObject.CompareTag ("Exittrigger")) {
			
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

		else if (other.gameObject.CompareTag ("Button_trigger_level1")) {
			other.GetComponent<level1Switch>().Triggered();
		}
		else if (other.gameObject.CompareTag ("Button_trigger") && notActivate) {
			notActivate = false;
			keys = false;
			tapLocation = transform.position;
			rb.velocity = Vector3.zero;
			other.GetComponent<Trigger_Controller>().Triggered();
		}

	}


	void OnTriggerStay(Collider other){
		if (other.gameObject.CompareTag ("Rotator_trigger")) {
			Rotator Rotator_script = other.GetComponent<Rotator>();
			Rotator_script.rotator_triggered();
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
		

	IEnumerator respawn()
	{
		//StartCoroutine(ShowMessage(ExitWarning, "You Shall Not Embrace the Light", 4));
		//print(Time.time);
		yield return new WaitForSeconds(2);
		tapLocation = original_pos;
		keys = true;
		transform.position = original_pos;
		for (int i = 0; i < movedObjects.Length; i++)
		{
			movedObjects[i].transform.position = movedOjectsPosition[i];
		}
		anim.SetBool("isDead", false);
	}
}
