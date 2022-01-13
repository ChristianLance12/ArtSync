using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSwap : MonoBehaviour
{
   public Dropdown dropDown;
    

    // Update is called once per frame
    public void OnValueChange()
    {
        QualitySettings.SetQualityLevel(dropDown.value, true);

    }
}
