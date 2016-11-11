using UnityEngine;
using System.Collections;

public class InstantiateWitch : MonoBehaviour {
    public GameObject witchPrefab;
    public Transform witchPosition;


	// Use this for initialization
	void Start () {
        Instantiate(witchPrefab, witchPosition.position, witchPosition.rotation);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
