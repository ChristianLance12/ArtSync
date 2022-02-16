using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyInspect : MonoBehaviour
{
    public Type type;
    public bool inRange;
    public int position;
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
            if (type == Type.ART) 
            {
                DataCollectArt();
            }
            else
            {
                DataCollectObj();
            }
            pM.footSteps1.Pause();
            pM.footSteps2.Pause();
            Cursor.lockState = CursorLockMode.None;
            if (view != null)
            {
                view.SetActive(true);
            }
            gM.viewing = true;
            gM.selected = this.gameObject;
#if !UNITY_EDITOR
            if (type == Type.ART)
            {
                WebGLPluginJS.EmptyInspect(data);
            }
            if (type == Type.LOBJ)
            {
                WebGLPluginJS.EmptyLargeInspect(data);
            }
            if (type == Type.SOBJ)
            {
                WebGLPluginJS.EmptyInspect(data);
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
            if (view != null)
            {
                view.SetActive(false);
            }
            gM.viewing = false;
            if (inRange == false)
            {
                gM.viewtxt2.SetActive(false);
            }
#if !UNITY_EDITOR
            WebGLPluginJS.OnUnityUninspect();
#endif
            gM.selected = null;
        }
        //inspect ui prompt
        if ((inRange || this.gameObject == gM.selected) && gM.viewing)
        {
            gM.viewtxt2Text.text = "[Space] to cancel";

        }
        else if ((inRange || this.gameObject == gM.selected) && gM.viewing == false)
        {
            gM.viewtxt2Text.text = "[Space] to add art";

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player") && this.enabled == true)
        {
            inRange = true;
            gM.viewtxt2.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inRange = false;
            gM.viewtxt2.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gM.viewing == false && this.enabled)
        {
            inRange = true;
            gM.viewtxt2.SetActive(true);
        }
    }
    private void OnDisable()
    {
        inRange = false;
        if (gM.viewtxt2 != null)
        {
            gM.viewtxt2.SetActive(false);
        }
    }
    public void DataCollectArt()
    {
        data = ("," + "," + position + ",");
        Debug.Log(data);
    }
    public void DataCollectObj()
    {
        data = (position + "," + ",");
        Debug.Log(data);
    }
    void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.GetChild(0).position, direction);
    }
}


