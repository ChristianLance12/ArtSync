using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    public PlayerSettings pS;
    private void OnDisable()
    {
        pS.SaveSettings();
    }
}
