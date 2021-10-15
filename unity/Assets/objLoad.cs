using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class objLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetMesh());
    }
    //https://www.cgtrader.com/items/72531/free-downloads/238325
    // Update is called once per frame
    void Update()
    {
    
    }
    IEnumerator GetMesh()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://www.cgtrader.com/items/72531/free-downloads/238325");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            var myMesh = ((DownloadHandlerFile)www.downloadHandler).data;
            GetComponent<MeshFilter>().mesh = myMesh;
        }
    }
}
