using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLPluginJS : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void OnUnityInspect(string jsonString);

    [DllImport("__Internal")]
    public static extern void OnUnityUninspect();

    [DllImport("__Internal")]
    public static extern void OnUnityPause();

    [DllImport("__Internal")]
    public static extern void OnUnityUnpause();

    [DllImport("__Internal")]
    public static extern void EmptyInspect();


}