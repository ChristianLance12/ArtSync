using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveRender : MonoBehaviour
   
{
    private GameController gM;
    public bool pedestalObj;
   
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if (gM.editor == false)
        {
            this.gameObject.SetActive(false);
        }
        else if (gM.editor == false && pedestalObj == true)
        {
            GetComponent<EmptyInspect>().enabled = false;
        }
    }

}
