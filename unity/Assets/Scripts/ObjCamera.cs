using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjCamera : MonoBehaviour
{
    private GameController gM;
    public float distance;
    private Vector3 previousLoc;
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
                transform.Translate(Vector3.right * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime);
            } 
        }
        if (sObj == true)
        {
           
                distance += Vector3.Distance(transform.position, previousLoc);
                previousLoc = transform.position;            
            if ((distance >= 16.58576 && firstRotDone == false && switcher == false ) || (distance >= 33.17152 && switcher == false))
            {
                StartCoroutine(changeBool());
                
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
