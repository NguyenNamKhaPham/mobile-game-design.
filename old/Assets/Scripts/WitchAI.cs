using UnityEngine;
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
        transform.rotation = new Quaternion(0f, 0f, 0f, 1);
        start = temp;
        temp.z += 100f;
        stop1 = temp;

        temp.z -= 50f;
        stop2 = temp;
        temp.x -= 90f;
        end = temp;

        //set step
        moveStep = moveSpeed * Time.deltaTime;
        rotateStep = rotateSpeed * Time.deltaTime;
        moveSpeed = 5f;
        rotateSpeed = 40f;
        pathFlag = 0;
        isUp = true;
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
                //print("stop1");
            }
            else if (stage == 1)
            {
                isUp = false;
                rotation(180f);
                //print("180");
            }
            else if (stage == 2)
            {
                
                moveTo(start);
                //print("start");
            }
            else if (stage == 3)
            {   
                isUp = true;
                rotation(0f);
                //print("0");
            }
        }
        //pathFlag == 1 stop
        else if(pathFlag == 2)
        {   
			//print("11111111");
            if (subStage)
            {
                stage = 0;
                subStage = false;
            }
            else if (stage == 0)
            {
                rotatePath();
            }
            else if (stage == 1)
            {
                moveTo(stop2);
            }
            else if(stage == 2)
            {
                rotation(-90f);
            }
            else if(stage == 3)
            {
                pathFlag = 3;
            }
        }
        else if (pathFlag == 3)
        {   
            if (!subStage)
            {
                stage = 0;
                subStage = true;
            }
            //move to path
            //print(stage);
            if (stage == 0)
            {
                moveTo(end);
            }
            else if (stage == 1)
            {
				finish = true;
                rotation(90f);
            }
            else if (stage == 2)
            {
                moveTo(stop2);
            }
            else if (stage == 3)
            {
                rotation(-90f);
                
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
        //print("inside");
        anim = GetComponent<Animator>();
        anim.SetBool("isWalking", true);
        rotating = false;
        
        transform.position = Vector3.MoveTowards(transform.position, location, moveStep);
        //Debug.Log (transform.position);
        //Debug.Log (location);
        

        if ((Mathf.Abs(transform.position.x - location.x) < 0.1f) && (Mathf.Abs(transform.position.z - location.z) < 0.1f))
        {
            rotating = true;
            //Debug.Log ("11111111");
        }

        //Debug.Log (rotating);
    }

    void rotation(float a)
    {
        rotating = false;
        //anim.SetBool("isWalking", false);
        //prev = transform.rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0f, a, 0f), rotateStep);
        //Debug.Log (transform.rotation);
        //Debug.Log (Quaternion.Euler(0f,a,0f));

        if (Quaternion.Euler(0f, a, 0f) == transform.rotation)
        {
            rotating = true;
            //Debug.Log (transform.rotation);
            //Debug.Log (Quaternion.Euler(0f,a,0f));
            //Debug.Log ("222222222222");
        }
        //Debug.Log (rotating);
    }
    void rotatePath()
    {
        if (transform.position.z - start.z < (end.z - start.z) / 2 && !isUp)
        {
            rotation(0f);
        }
        else if (transform.position.z - start.z > (end.z - start.z) / 2 && isUp)
        {
            rotation(180f);
        }
    }
}



