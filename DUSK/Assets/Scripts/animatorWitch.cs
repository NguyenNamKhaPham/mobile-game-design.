using UnityEngine;
using System.Collections;

public class animatorWitch : MonoBehaviour {

    Animator anim;
    public bool isWalking = false;
    public bool isScanning = false;
    public bool isDetect = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
        
        if (isDetect)
        {
            anim.SetBool("isDetect", true);
        }
        else
        {
            anim.SetBool("isDetect", false);
        }
			
        if (isWalking)
        {
            print("isWalking");
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (isScanning)
        {
            anim.SetBool("isScanning", true);
        }
        else
        {
            anim.SetBool("isScanning", false);
        }
    }

}
