using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveRender : MonoBehaviour
   
{
    private GameController gM;
   
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        if (gM.editor == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
