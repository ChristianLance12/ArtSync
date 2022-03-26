using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestConsole : MonoBehaviour
{
    public bool fullScene;
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
    public PlayerSettings pS;
    private ArtLoad aL;
    private ObjFromStream oL;
    private SmallObjFromStream sL;
    private GameController gM;
    void Awake()
    {


        #if !UNITY_EDITOR
        fullScene = false;
#endif

        aL = GameObject.FindWithTag("GameController").GetComponent<ArtLoad>();
        oL = GameObject.FindWithTag("GameController").GetComponent<ObjFromStream>();
        sL = GameObject.FindWithTag("GameController").GetComponent<SmallObjFromStream>();
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    void Start()
    {
        #if !UNITY_EDITOR
        this.gameObject.SetActive(false);
#endif
        ArtSize.text = "0";
        ArtFrame.text = "0";
        ArtURL.text = "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fi.ytimg.com%2Fvi%2FlQezinb283E%2Fmaxresdefault.jpg&f=1&nofb=1";
        ObjText.text = "0";
        ObjURL.text = "https://groups.csail.mit.edu/graphics/classes/6.837/F03/models/cow-nonormals.obj";
        ObjSText.text = "0";
        ObjSURL.text = "https://groups.csail.mit.edu/graphics/classes/6.837/F03/models/teapot.obj";
        if (fullScene == true)
        {
            fullSceneLoad();
        }
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
    public void fullSceneLoad()
    {
        for (int i = 0; i < aL.artSpawns.Length; i++)
        {
            
            int size = Random.Range(0, aL.frameDimension.Length);
            int frame = Random.Range(0, aL.frames.Length);
            string data = size + "," + frame + "," + i + "," + ArtURL.text;
            aL.ArtJson(data);
        }
        for (int i = 0; i < oL.objSpawns.Length; i++)
        {

            int texture = Random.Range(0, gM.textures.Length);
          
            string data = i + "," + texture + "," + ObjURL.text;
            oL.ObjJson(data);
        }
        for (int i = 0; i < sL.objSpawns.Length; i++)
        {

            int texture = Random.Range(0, gM.textures.Length);
            string data = i + "," + texture + "," + ObjSURL.text;
            sL.ObjSJson(data);
        }
    }
}
