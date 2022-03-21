using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLPluginJS : MonoBehaviour
{

    [DllImport("__Internal")]
    public static extern void OnUnityInspect(string detail);

    [DllImport("__Internal")]
    public static extern void OnUnityLargeInspect(string detail);

    [DllImport("__Internal")]
    public static extern void OnUnitySmallInspect(string detail);

    [DllImport("__Internal")]
    public static extern void OnUnityUninspect();

    [DllImport("__Internal")]
    public static extern void OnUnityPause();

    [DllImport("__Internal")]
    public static extern void OnUnityUnpause();

    [DllImport("__Internal")]
    public static extern void EmptyInspect(string detail);

    [DllImport("__Internal")]
    public static extern void EmptyLargeInspect(string detail);

    [DllImport("__Internal")]
    public static extern void EmptySmallInspect(string detail);

    [DllImport("__Internal")]
    public static extern void SaveSettings(string detail);

}