using UnityEngine;
using System.Collections;

public class animatorWitch : MonoBehaviour {

    Animator anim;
    public bool isWalking = false;
    public bool isTurning = false;
    public bool isScanning = false;
    public bool isDetect = false;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update() {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isDetect", true);
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("isTurning", true);
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("isWalking", true);
        } else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("isScanning", true);
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("isDetect", false);
        } else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("isTurning", false);
        } else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetBool("isWalking", false);
        } else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("isScanning", false);
        }
        */
        print(isWalking);
        if (isDetect)
        {
            anim.SetBool("isDetect", true);
        }
        else
        {
            anim.SetBool("isDetect", false);
        }

        if (isTurning)
        {
            anim.SetBool("isTurning", true);
        }
        else
        {
            anim.SetBool("isTurning", false);
        }

        if (isWalking)
        {
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
