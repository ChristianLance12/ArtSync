using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCamera : MonoBehaviour
{
    private GameController gM;
    public float distance;
    private Vector3 previousLoc;
    private float rotDist = 4f;
    private bool right;
    private bool firstRotDone;
    public bool sObj;
    private bool switcher;
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        right = true;       
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(gameObject.transform.parent.GetChild(0));
        if (gM.loadingScreenOn == false) 
        {
            if (right == true)
            {
                if (sObj == true)
                {
                    transform.Translate(Vector3.right * Time.deltaTime * 0.65f);
                }
                else
                {
                    transform.Translate(Vector3.right * Time.deltaTime);
                }
            }
            else
            {
                if (sObj == true)
                {
                    transform.Translate(Vector3.left * Time.deltaTime * 0.65f);
                }
                else
                {
                    transform.Translate(Vector3.left * Time.deltaTime);
                }
            }
            if (sObj == true)
            {
                if (previousLoc.x != 0 && previousLoc.y != 0 && previousLoc.z != 0)
                {
                    distance += Vector3.Distance(transform.position, previousLoc);
                }
                
                previousLoc = transform.position;
                if ((distance >= rotDist && firstRotDone == false && switcher == false) || (distance >= (rotDist*2) && switcher == false))
                {
                    StartCoroutine(changeBool());

                }
            }

        }

    }
    private IEnumerator changeBool()
    {
        if (firstRotDone == false)
        {
            firstRotDone = true;
        }
        switcher = true;
        right = !right;
        distance = 0;
        yield return new WaitForSeconds(0.1f);
        switcher = false;
    }
   
}
