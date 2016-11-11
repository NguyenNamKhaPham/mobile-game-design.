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

	public int keys;

	//for shadow detection
	private ShadowDetector sd;

	//For movement
	public GameObject tap;
	private Vector3 tapLocation;
	private Ray ray; 
	private Rigidbody rb;
	public float speed;

	//transparent
	private ArrayList oldGS = new ArrayList();
	private GameObject[] newGS;

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
			
		ExitWarning.enabled = false;

		if (ending_screen.gameObject.activeInHierarchy == true) {
			ending_screen.gameObject.SetActive (false);
		}

		//set candy
		SetCountText ();
		candynum = 0;


	}


	void FixedUpdate () {
		//hit light
		if (!sd.isShaded) {
			Debug.Log ("die");
			/*
			//pop up warning, pumpkin stops and resqpawns, movable objects respawn
			StartCoroutine (ShowMessage (ExitWarning, "You Shall Not Embrace the Light", 4));
			transform.position = original_pos;
			tapLocation = original_pos;
			rb.velocity = Vector3.zero;
			for (int i = 0; i < movedObjects.Length; i++) {
				movedObjects [i].transform.position = movedOjectsPosition [i];
			}
			*/

		}

		if (keys == 0) {
			speed = 2f;
			bool a = false;
			bool b = false;
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			Vector3 moveVector = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			Vector3 faceTo = new Vector3 (-moveHorizontal, 0.0f, -moveVertical);
			if (moveVector != Vector3.zero)
				transform.rotation = Quaternion.LookRotation (faceTo);
			rb.velocity = moveVector * speed;
		} else if (keys == 1) {
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

			//get to that location
			if ((Mathf.Abs (transform.position.x - tapLocation.x) > 0.1f) || (Mathf.Abs (transform.position.z - tapLocation.z) > 0.1f)) {
				rb.velocity = (tapLocation - transform.position) * speed;
			} else {
				tap.SetActive (false);
			}
		} else if (keys == 2) {
			
		}
		makeTransparent ();
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

		if (other.gameObject.CompareTag ("Button_trigger_level1")) {
			level1Switch Trigger_Controller_script = other.GetComponent<level1Switch>();
			Trigger_Controller_script.Triggered();
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
		

	//check of GameObject g in array
	bool exists(GameObject g, GameObject[] gs){
		int i = gs.Length;
		//Debug.Log (i);
		//Debug.Log (g);
		while (i > 0) {
			i -= 1;
			//Debug.Log (gs[i]);
			if (g == gs [i])
				return true;
		}
		return false;
	}

	//check of GameObject g in array
	bool exists(GameObject g, ArrayList gs){
		int i = gs.Count;
		//Debug.Log (i);
		//Debug.Log (g);
		while (i > 0) {
			i -= 1;
			//Debug.Log (gs[i]);
			if (g == gs [i])
				return true;
		}
		return false;
	}

	void makeTransparent(){
		//collect all blocked objects
		Vector3 fwd = Camera.main.transform.position - transform.position;
		Vector3 temp = transform.position;
		RaycastHit[] hits;
		hits = Physics.RaycastAll (temp, fwd, 50f);
		newGS = new GameObject[hits.Length];
		for (int i = 0; i < hits.Length; i++) {
			newGS [i] = hits[i].collider.gameObject;
		}

		//old object no longer blocks
		//Debug.Log ("aaaaaaa");
		foreach (GameObject oldG in oldGS){
			if (!exists (oldG, newGS) && oldG.tag == "remove"){
				//Debug.Log (oldG);
				oldG.GetComponent<MeshRenderer> ().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				oldGS.Remove (oldG);
			}
		}
		//new objects block
		//Debug.Log ("bbbbbbb");
		foreach (GameObject newG in newGS) {
			if (!exists (newG, oldGS) && newG.tag == "remove") {
				//Debug.Log (newG);
				oldGS.Add (newG);
				newG.GetComponent<MeshRenderer> ().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
			}
		}
	}
}
