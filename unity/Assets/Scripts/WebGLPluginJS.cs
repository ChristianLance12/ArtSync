using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLPluginJS : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void OnUnityInspect(string jsonString);

    [DllImport("__Internal")]
    public static extern void OnUnityLargeInspect(string jsonString);

    [DllImport("__Internal")]
    public static extern void OnUnitySmallInspect(string jsonString);

    [DllImport("__Internal")]
    public static extern void OnUnityUninspect();

    [DllImport("__Internal")]
    public static extern void OnUnityPause();

    [DllImport("__Internal")]
    public static extern void OnUnityUnpause();

    [DllImport("__Internal")]
    public static extern void EmptyInspect(string position);

    [DllImport("__Internal")]
    public static extern void EmptyLargeInspect(string position);

    [DllImport("__Internal")]
    public static extern void EmptySmallInspect(string position);

}