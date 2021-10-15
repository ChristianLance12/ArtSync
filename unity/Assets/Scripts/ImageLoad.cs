using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;
public class ImageLoad : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(GetTexture());
    }

    IEnumerator GetTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://i.kym-cdn.com/entries/icons/original/000/026/152/gigachad.jpg");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            GetComponent<Renderer>().material.mainTexture = myTexture;
        }
    }                       // put the downloaded image file into the new Texture2D
               // put the new image into the current material as defuse material for testing.

    }
