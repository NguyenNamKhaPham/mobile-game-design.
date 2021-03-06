﻿using UnityEngine;
using System.Collections;

public class WitchAI : MonoBehaviour
{
	public Vector3 start;
	public Vector3 stop1;
	public Vector3 stop2;
	public Vector3 end;
	public int pathFlag;
	public bool finish;

	public float moveSpeed = 20f;
	public float rotateSpeed = 70f;


	private float moveStep;
	private float rotateStep;
	private bool rotating;
	private bool rot;
	//private Quaternion prev;
	private int stage = 0;
	private bool isUp;
	private bool rotateDown;
	private bool subStage;


	Animator anim;

	void Start()
	{
		//set location
		Vector3 temp = transform.position;
		//transform.rotation = new Quaternion(0f, 90f, 0f, 1);
        print(transform.rotation);
		start = temp;
		temp.x += 100f;
		stop1 = temp;

		temp.x -= 50f;
		stop2 = temp;
        temp.z -= 40f;
		end = temp;

		//set step
		moveStep = moveSpeed * Time.deltaTime;
		rotateStep = rotateSpeed * Time.deltaTime;
		moveSpeed = 5f;
		rotateSpeed = 40f;
		pathFlag = 0;
		rotateDown = false;
		subStage = true;
		finish = false;
	}

	void Update()
	{ 
		//vertical
		if (pathFlag == 0)
		{
			if (stage == 0)
			{

				moveTo(stop1);
				////print("stop1");
			}
			else if (stage == 1)
			{
				rotation(-90f);
				////print("180");
			}
			else if (stage == 2)
			{

				moveTo(start);
				////print("start");
			}
			else if (stage == 3)
			{   
				rotation(90f);
				////print("0");
			}
		}
		//pathFlag == 1 stop
		else if(pathFlag == 2)
		{   
			//print("11111111");
			if (subStage)
			{//print("2222");
				stage = 0;
				subStage = false;
			}
			else if (stage == 0)
			{//print("3333");
				rotatePath();
			}
			else if (stage == 1)
			{//print("444");
                moveTo(stop2);
			}
			else if(stage == 2)
			{//print("5555");
				rotation(180f);
			}
			else if(stage == 3)
			{//print("6666");
				pathFlag = 3;
			}
		}
		else if (pathFlag == 3)
		{   //print("000000");
			if (!subStage)
			{
				stage = 0;
				subStage = true;
			}
			//move to path
			////print(stage);
			if (stage == 0)
			{
                stop2.z = 150;
                moveTo(end);
			}
			else if (stage == 1)
			{
				finish = true;
				rotation(0f);
			}
			else if (stage == 2)
			{
				moveTo(stop2);
			}
			else if (stage == 3)
			{
				rotation(180f);

			}
		}



		if (rotating)
		{
			rotating = false;
			stage += 1;
			if (stage == 4)
				stage = 0;
			//Debug.Log ("000000000000000000000");
		}

	}

	void moveTo(Vector3 location)
	{
		anim = GetComponent<Animator>();
		anim.SetBool("isWalking", true);
		rotating = false;

		transform.position = Vector3.MoveTowards(transform.position, location, moveStep);


		if ((Mathf.Abs(transform.position.x - location.x) < 0.1f) && (Mathf.Abs(transform.position.z - location.z) < 0.1f))
		{
			rotating = true;
		}
	}

	void rotation(float a)
	{
		rotating = false;
		transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, a, 0f), rotateStep);

		if (Quaternion.Euler(0f, a, 0f) == transform.rotation)
		{
			rotating = true;
		}
	}
	void rotatePath()
	{
		if (transform.position.x - start.x < (end.x - start.x) / 2) {
			rotation (90f);
		} else if (transform.position.x - start.x > (end.x - start.x) / 2) {
			rotation (-90f);
		} else {
			stage = 1;
		}
	}
}



