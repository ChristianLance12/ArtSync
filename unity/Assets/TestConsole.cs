using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestConsole : MonoBehaviour
{
    public InputField ObjSPos;
    public InputField ObjSText;
    public InputField ObjSURL;
    public InputField ObjPos;
    public InputField ObjText;
    public InputField ObjURL;
    public InputField ArtPos;
    public InputField ArtFrame;
    public InputField ArtSize;
    public InputField ArtURL;
    private ArtLoad aL;
    private ObjFromStream oL;
    private SmallObjFromStream sL;
    void Start()
    {
#if !UNITY_EDITOR
        this.gameObject.SetActive(false);
       
#endif
        aL = GameObject.FindWithTag("GameController").GetComponent<ArtLoad>();
        oL = GameObject.FindWithTag("GameController").GetComponent<ObjFromStream>();
        sL = GameObject.FindWithTag("GameController").GetComponent<SmallObjFromStream>();
        
        ArtSize.text = "0";
        ArtFrame.text = "0";
        ArtURL.text = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.ytimg.com%2Fvi%2FlQezinb283E%2Fmaxresdefault.jpg&f=1&nofb=1";
        ObjText.text = "0";
        ObjURL.text = "https://groups.csail.mit.edu/graphics/classes/6.837/F03/models/cow-nonormals.obj";
        ObjSText.text = "0";
        ObjSURL.text = "https://groups.csail.mit.edu/graphics/classes/6.837/F03/models/teapot.obj";
    }
    public void TestLoadArt()
    {
        try
        {
            string data = (ArtSize.text + "," + ArtFrame.text + "," + ArtPos.text + "," + ArtURL.text);
            aL.ArtJson(data);
        }
        catch
        {
            Debug.Log("INVALID PARAMETERS");
        }
    }
    public void TestLoadObj()
    {
        try
        {
            string data = (ObjPos.text + "," + ObjText.text + "," + ObjURL.text);
            oL.ObjJson(data);
        }
        catch
        {
            Debug.Log("INVALID PARAMETERS");
        }
    }
    public void TestLoadObjS()
    {
        try 
        { 
            string data = (ObjSPos.text + "," + ObjSText.text + "," + ObjSURL.text);
            sL.ObjSJson(data);
        }
        catch
        {
            Debug.Log("INVALID PARAMETERS");
        }
    }
    public void DeleteArt()
    {
        try
        {
            
            aL.ArtDeleteJson(ArtPos.text);
        }
        catch
        {
            Debug.Log("INVALID PARAMETERS");
        }
    }
    public void DeleteObj()
    {
        try
        {

            oL.ObjDeleteJson(ObjPos.text);
        }
        catch
        {
            Debug.Log("INVALID PARAMETERS");
        }
    }
    public void DeleteObjS()
    {
        try
        {

            sL.ObjSDeleteJson(ObjSPos.text);
        }
        catch
        {
            Debug.Log("INVALID PARAMETERS");
        }
    }
}
