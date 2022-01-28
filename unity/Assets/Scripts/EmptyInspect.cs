using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyInspect : MonoBehaviour
{
    public Type type;
    private bool inRange;
    public int position;
    public string positionS;
    private GameController gM;
    public GameObject view;

    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        positionS = position.ToString();

    }

    void Update()
    {
        //inspect/view action
        if (inRange && Input.GetKeyDown(KeyCode.Space) && gM.viewing == false && gM.paused == false)
        {
            Cursor.lockState = CursorLockMode.None;
            if (view != null)
            {
                view.SetActive(true);
            }
            gM.viewing = true;

#if !UNITY_EDITOR
            if (type == Type.ART)
            {
                WebGLPluginJS.EmptyInspect(positionS);
            }
            if (type == Type.LOBJ)
            {
                WebGLPluginJS.EmptyLargeInspect(positionS);
            }
            if (type == Type.SOBJ)
            {
                WebGLPluginJS.EmptyInspect(positionS);
            }
#endif


        }
        else if (inRange && Input.GetKeyDown(KeyCode.Space) && gM.viewing == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (view != null)
            {
                view.SetActive(false);
            }
            gM.viewing = false;
#if !UNITY_EDITOR
            WebGLPluginJS.OnUnityUninspect();
#endif

        }
        //inspect ui prompt
        if (inRange && gM.viewing)
        {
            gM.viewtxt2Text.text = "[Space] to cancel";

        }
        else if (inRange && gM.viewing == false)
        {
            gM.viewtxt2Text.text = "[Space] to add art";

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && this.enabled == true)
        {
            inRange = true;
            gM.viewtxt2.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            gM.viewtxt2.SetActive(false);
        }
    }
    private void OnDisable()
    {
        if (gM.viewtxt2 != null)
        {
            gM.viewtxt2.SetActive(false);
        }
    }
}


