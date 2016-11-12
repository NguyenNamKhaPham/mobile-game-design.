using UnityEngine;
using System.Collections;

public class InstantiateWitch : MonoBehaviour {
    public GameObject witchPrefab;
    public Transform witchPosition;


	// Use this for initialization
	void Start () {
        Instantiate(witchPrefab, witchPosition.position, Quaternion.Euler(0, 90, 0));
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
