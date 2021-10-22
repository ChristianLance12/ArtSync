using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspect : MonoBehaviour
{
   public bool inRange;
   private GameController gM;
    public GameObject view;
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        //inspect/view action
        if (inRange && Input.GetKeyDown(KeyCode.Space) && gM.viewing == false && gM.paused == false) 
        {
            Cursor.lockState = CursorLockMode.None;
            view.SetActive(true);
            gM.viewing = true;
            
        }
        else if (inRange && Input.GetKeyDown(KeyCode.Space) && gM.viewing)
        {
            Cursor.lockState = CursorLockMode.Locked    ;
            gM.viewing = false;
            view.SetActive(false);
        }
        //inspect ui prompt
        if (inRange && gM.viewing == false)
        {
            gM.viewtxt.SetActive(true);
            gM.viewtxt2.SetActive(false);
        }
        else if (inRange && gM.viewing)
        {
            gM.viewtxt.SetActive(false);
            gM.viewtxt2.SetActive(true);
        }
        else
        {
            gM.viewtxt.SetActive(false);
            gM.viewtxt2.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
}
