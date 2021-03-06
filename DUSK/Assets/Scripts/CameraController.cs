﻿using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject Player;
	public Light l1;
	public GameObject l3;
	private GameObject l2;
	private Vector3 offset;
    private Vector3 pumkinPos;
	public bool level2;
	private Vector3 v;
	private Vector3 v1;
	public int i = 0;
	private Vector3 s;
	private bool q = true;
	private WitchAI w;
	public bool unlockALock;
	public GameObject lock1;
	public GameObject lock2;
	public GameObject lock3;
	public GameObject door;
	public Vector3 doorPos;
	public GameObject magic;
	public GameObject pointToDoor;
	public int j;

	//transparent
	private ArrayList oldGS = new ArrayList();
	private GameObject[] newGS;

	// Use this for initialization
	void Start () {
		offset = new Vector3 (0, 60, -45);
		pumkinPos = Player.transform.position;
		transform.position = pumkinPos + offset;
		transform.LookAt (pumkinPos);
		v = l1.gameObject.transform.position;
		v.y = transform.position.y;
		v.z -= 15f;
		l2 = GameObject.Find ("Witch_Model_Prefab(Clone)");
		w = l2.GetComponent<WitchAI> ();
	}

	// Update is called once per frame
	void LateUpdate () {
		//Debug.Log ("----MAKE TRANS----");
		makeTransparent ();
		if (level2) {
			switchOfflevel2 ();
		} else if (unlockALock) {
			unlock ();
		} else {
			pumkinPos = Player.transform.position;
			transform.position = pumkinPos + offset;
			transform.LookAt (pumkinPos);
			pointToDoor.transform.LookAt (lock2.transform.position);
			//Debug.Log (offset);
		}
    }


	void unlock(){
		if (j == 0) {
			s = transform.position;
			doorPos = door.transform.position;
			doorPos.y += 30;
			doorPos.z -= 30;
			j++;
			q = true;
			StartCoroutine_Auto (wait1 (0.5f));
		} else if (j == 2) {
			transform.position = Vector3.MoveTowards (transform.position, doorPos, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - doorPos.x) < 0.1f) && (Mathf.Abs (transform.position.z - doorPos.z) < 0.1f)) {
				j++;
				q = true;
				StartCoroutine_Auto (wait1 (0.5f));
			}
		} else if (j == 4) {
			if (lock1.activeSelf) {
				lock1.GetComponent<Animator> ().SetBool ("isUnlocked", true);
				StartCoroutine_Auto (unlockAni (1.5f, 1));
				j++;
				q = true;
				StartCoroutine_Auto (wait1 (2));
			} else if (lock2.activeSelf) {
				lock2.GetComponent<Animator> ().SetBool ("isUnlocked", true);
				StartCoroutine_Auto (unlockAni (1, 2));
				j++;
				q = true;
				StartCoroutine_Auto (wait1 (2));
			} else if (lock3.activeSelf){
				lock3.GetComponent<Animator> ().SetBool ("isUnlocked", true);
				StartCoroutine_Auto (unlockAni (1, 3));
				j++;
				q = true;
				StartCoroutine_Auto (wait1 (3));
			}

		} else if (j == 6) {
			transform.position = Vector3.MoveTowards (transform.position, s, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - s.x) < 0.1f) && (Mathf.Abs (transform.position.z - s.z) < 0.1f)) {
				unlockALock = false;
				GameObject.Find ("Player").GetComponent<PlayerController> ().keys = true;
				j = 0;
			}
		}

	}

	IEnumerator unlockAni(float f, int l){
		yield return new WaitForSeconds (f);
		if (l == 1)
			lock1.SetActive (false);
		else if (l == 2)
			lock2.SetActive (false);
		else {
			lock3.SetActive (false);
			door.GetComponent<Animator> ().SetBool ("isUnlocked", true);
			magic.SetActive(true);
		}
	}

	IEnumerator wait1(float f){
		yield return new WaitForSeconds (f);
		if (q) {
			j++; 
			q = false;
		}
	}

	IEnumerator wait(float f){
		yield return new WaitForSeconds (f);
		if (q) {
			i++; 
			q = false;
		}
	}

	void makeTransparent(){
		//collect all blocked objects
		RaycastHit[] hits;
		hits = Physics.RaycastAll (transform.position, -offset, 100f);
		newGS = new GameObject[hits.Length];
		for (int i = 0; i < hits.Length; i++) {
			newGS [i] = hits[i].collider.gameObject;
		}
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
		while (i > 0) {
			i -= 1;
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

	void switchOfflevel2(){
		if (i == 0) {
			s = transform.position;
			i++;
			q = true;
			StartCoroutine_Auto (wait (0.5f));
		} else if (i == 2) {
			transform.position = Vector3.MoveTowards (transform.position, v, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - v.x) < 0.1f) && (Mathf.Abs (transform.position.z - v.z) < 0.1f)) {
				i++;
				q = true;
				StartCoroutine_Auto (wait (1));
			}
		} else if (i == 4) {
			l1.gameObject.SetActive (false);
			i++;
			q = true;
			StartCoroutine_Auto (wait (1));
		} else if (i == 6) {
			w.pathFlag = 1;
			i++;
		} else if (i == 7) {
			v1 = l2.transform.position;
			v1.y += 40f;
			v1.z -= 23f;
			i++;
		} else if (i == 8) {
			transform.position = Vector3.MoveTowards (transform.position, v1, 100f * Time.deltaTime);
			if ((Mathf.Abs (transform.position.x - v1.x) < 0.1f) && (Mathf.Abs (transform.position.z - v1.z) < 0.1f)) {
				i++;
			}
		} else if (i == 9) {

			i++;
			q = true;
			StartCoroutine_Auto (wait (3));
		} else if (i == 11) {
			w.pathFlag = 2;
			i++;
		} else if (i == 12) {
			v1 = l2.transform.position;
			v1.y += 40f;
			v1.x -= 23f;
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
