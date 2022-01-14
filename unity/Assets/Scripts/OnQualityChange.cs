using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnQualityChange : MonoBehaviour
{
    public Dropdown dd;
    public void OnValueChange()
    {
        QualitySettings.SetQualityLevel(dd.value , true);
    }
}
