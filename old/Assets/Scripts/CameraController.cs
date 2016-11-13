using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	public Light l1;
	public GameObject l3;
	private GameObject l2;
	public Vector3 offset;
    private Vector3 pumkinPos;
	public bool level2;
	private Vector3 v;
	private Vector3 v1;
	public int i = 0;
	private Vector3 s;
	private bool q = true;
	private WitchAI w;

	//transparent
	private ArrayList oldGS = new ArrayList();
	private GameObject[] newGS;

	// Use this for initialization
	void Start () {
		offset = new Vector3(0, 60, -45);
		pumkinPos = Player.transform.position;
		transform.position = pumkinPos + offset;
		transform.LookAt(pumkinPos);
		v = l1.gameObject.transform.position;
		v.y = transform.position.y;
		v.z -= 15f;
		l2 = GameObject.Find ("Witch_Model_Prefab(Clone)");
		w = l2.GetComponent<WitchAI> ();

	}

	void update(){
		
	}

	// Update is called once per frame
	void LateUpdate () {
		makeTransparent ();
		if (!level2) {
			pumkinPos = Player.transform.position;
			transform.position = pumkinPos + offset;
			transform.LookAt (pumkinPos);
			//Debug.Log (offset);
		} else {
			if (i == 0) {
				s = transform.position;
				i++;
				q = true;
				StartCoroutine_Auto( wait (1));
			} else if (i == 2) {
				transform.position = Vector3.MoveTowards (transform.position, v, 30f * Time.deltaTime);
				if ((Mathf.Abs (transform.position.x - v.x) < 0.1f) && (Mathf.Abs (transform.position.z - v.z) < 0.1f)) {
					i++;
					q = true;
					StartCoroutine_Auto( wait (1));
				}
			} else if (i == 4) {
				l1.gameObject.SetActive (false);
				i++;
				q = true;
				StartCoroutine_Auto( wait (1));
			} else if (i == 6){
				w.pathFlag = 1;
				i++;
			} else if (i == 7){
				v1 = l2.transform.position;
				v1.y += 40f;
				v1.z -= 23f;
				i++;
			} else if (i == 8){
				transform.position = Vector3.MoveTowards (transform.position, v1, 30f * Time.deltaTime);
				if ((Mathf.Abs (transform.position.x - v1.x) < 0.1f) && (Mathf.Abs (transform.position.z - v1.z) < 0.1f)) {
					i++;
				}
			} else if (i == 9) {
				
				i++;
				q = true;
				StartCoroutine_Auto( wait (3));
			} else if (i == 11) {
				w.pathFlag = 2;
				i++;
			} else if (i == 12){
				v1 = l2.transform.position;
				v1.y += 40f;
				v1.z -= 23f;
				transform.position = Vector3.MoveTowards (transform.position, v1, 30f * Time.deltaTime);
				if (w.finish) {
					i++;
				}
			} else if (i == 13) {
				transform.position = Vector3.MoveTowards (transform.position, s, 30f * Time.deltaTime);
				if ((Mathf.Abs (transform.position.x - s.x) < 0.1f) && (Mathf.Abs (transform.position.z - s.z) < 0.1f)) {
					level2 = false;
					GameObject.Find ("Player").GetComponent<PlayerController> ().keys = true;
				}
			}
				

		}

    }

	IEnumerator wait(float f){
		Debug.Log (i);
		yield return new WaitForSeconds (f);
		if (q) {
			i++; 
			q = false;
		}
	}

	void makeTransparent(){
		//collect all blocked objects
		Debug.DrawRay(transform.position, transform.position - offset, Color.green);
		RaycastHit[] hits;
		hits = Physics.RaycastAll (transform.position, -offset, 100f);
		newGS = new GameObject[hits.Length];
		for (int i = 0; i < hits.Length; i++) {
			newGS [i] = hits[i].collider.gameObject;
		}
		Debug.Log (hits.Length);
		ArrayList t = new ArrayList();
		//old object no longer blocks
		//Debug.Log ("aaaaaaa");
		foreach (GameObject oldG in oldGS){
			if (!exists (oldG, newGS) && oldG.tag == "remove"){
				//Debug.Log (oldG);
				oldG.GetComponent<MeshRenderer> ().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
				t.Add (oldG);
			}
		}
		foreach (GameObject g in t) {
			oldGS.Remove (g);
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
}
