using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveRender : MonoBehaviour
   
{
    private GameController gM;
    public bool pedestalObj;
    private bool used;
    private bool usedCheckDone;
   
    void OnEnable()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if (gM.editor == false && pedestalObj == true)
        {
            usedCheck();
            GetComponent<EmptyInspect>().enabled = false;
        }
        else if (gM.editor == false)
        {
            this.gameObject.SetActive(false);
        }
        
    }
    private void Update()
    {
        if (pedestalObj == true && usedCheckDone == true && used == false)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void usedCheck()
    {
        foreach (GameObject obj in gM.loadedObjS)
        {
            if (this.gameObject.GetComponent<EmptyInspect>().position == obj.GetComponent<Inspect>().position)
            {

                used = true;
            }
           
        }
        usedCheckDone = true;
    }
}
