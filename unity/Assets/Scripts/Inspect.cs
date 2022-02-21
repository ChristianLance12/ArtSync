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
    private TestConsole tC;
    private PlayerMove pM;
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
#if UNITY_EDITOR
        tC = GameObject.FindWithTag("TestConsole").GetComponent<TestConsole>();
        pM = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
#endif

    }

    void Update()
    {
        //inspect/view action
        if ((inRange || this.gameObject == gM.selected) && Input.GetKeyDown(KeyCode.Space) && gM.viewing == false && gM.paused == false && gM.loadingScreenOn == false) 
        {
            Cursor.lockState = CursorLockMode.None;
            view.SetActive(true);
            gM.viewing = true;
            gM.selected = this.gameObject;
            pM.footSteps1.Pause();
            pM.footSteps2.Pause();

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
#if UNITY_EDITOR
            if (type == Type.ART)
            {
                tC.ArtPos.text = position.ToString();

            }
            if (type == Type.LOBJ)
            {
                tC.ObjPos.text = position.ToString();
            }
            if (type == Type.SOBJ)
            {
                tC.ObjSPos.text = position.ToString();
            }

#endif

        }
        else if ((inRange || this.gameObject == gM.selected) && Input.GetKeyDown(KeyCode.Space) && gM.viewing == true && gM.loadingScreenOn == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            view.SetActive(false);
            gM.viewing = false;
            gM.selected = null;
            if (inRange == false)
            {
                gM.viewtxt.SetActive(false);
            }
#if !UNITY_EDITOR
            WebGLPluginJS.OnUnityUninspect();
#endif

        }
        //inspect ui prompt
        if ((inRange || this.gameObject == gM.selected) && gM.viewing)
        {
            gM.viewtxtText.text = "[Space] to stop";

        }
        else if ((inRange || this.gameObject == gM.selected) && gM.viewing == false)
        {
            gM.viewtxtText.text = "[Space] to inspect";

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
        if (other.gameObject.CompareTag("Player"))
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
    private void OnDisable()
    {
        inRange = false;
        if (gM.viewtxt != null)
        {
            gM.viewtxt.SetActive(false);
        }
    }
}
