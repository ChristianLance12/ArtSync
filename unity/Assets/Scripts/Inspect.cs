using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type { ART, SOBJ, LOBJ }
public class Inspect : MonoBehaviour
{
   
   private bool inRange;
    public string url;
    public Type type;
    public int position;
    public int texture;
    public int frame;
    public int frameSize;
    public string data;
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

#if !UNITY_EDITOR
           if (type == Type.ART)
            {
                WebGLPluginJS.OnUnityInspect(data);
            }
            if (type == Type.LOBJ)
            {
                WebGLPluginJS.OnUnityLargeInspect(data);
            }
            if (type == Type.SOBJ)
            {
                WebGLPluginJS.OnUnitySmallInspect(data);
            }  
         
#endif

        }
        else if (inRange && Input.GetKeyDown(KeyCode.Space) && gM.viewing == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            view.SetActive(false);
            gM.viewing = false;
             #if !UNITY_EDITOR
            WebGLPluginJS.OnUnityUninspect();
#endif

        }
        //inspect ui prompt
        if (inRange && gM.viewing)
        {
            gM.viewtxtText.text = "[Space] to stop";

        }
        else if (inRange && gM.viewing == false)
        {
            gM.viewtxtText.text = "[Space] to inspect";

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
            gM.viewtxt.SetActive(true);
        }
        if (gM.viewing)
        {
            view.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            gM.viewtxt.SetActive(false);
        }
    }
    public void DataCollectArt()
    {
        data = (frameSize + "," + frame + "," + position + "," + url);
        Debug.Log(data);
    }
    public void DataCollectObj()
    {
        data = (position + "," + texture + "," + url);
        Debug.Log(data);
    }
}
