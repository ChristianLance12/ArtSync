using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverSound : MonoBehaviour, IPointerEnterHandler
{

    private GameController gM;
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gM.hoverUI.clip != null)
        {
            gM.hoverUI.Play();
        }
        else
        {
            Debug.Log("No sound");
        }
    }
}
