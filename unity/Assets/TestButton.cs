using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
    private ArtLoad aL;
    private ObjFromStream obj;
    public string jsonArt;
    // Start is called before the first frame update
    void Start()
    {
        aL = GameObject.FindWithTag("GameController").GetComponent<ArtLoad>();
        obj = GameObject.FindWithTag("GameController").GetComponent<ObjFromStream>();
    }

   public void ArtTest()
    {
        aL.ArtJson(jsonArt);
    }
    public void ObjTest()
    {
        obj.ObjJson(jsonArt);
    }
}
