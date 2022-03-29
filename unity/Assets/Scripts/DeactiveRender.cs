using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveRender : MonoBehaviour
   
{
    private GameController gM;
    public bool pedestalObj;
    public bool used;
    private void Awake()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    void OnEnable()
    {
       
        StartCoroutine(EditorCheck());
        
    }
  
    public IEnumerator EditorCheck()
    {
        yield return 0;
        if (gM.editor == false && used == false)
        {
            this.gameObject.SetActive(false);
        }
    }
   
}
