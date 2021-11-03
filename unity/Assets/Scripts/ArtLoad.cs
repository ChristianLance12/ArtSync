using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtLoad : MonoBehaviour
{
    // public string jSon;
    public Transform[] artSpawns;
    public GameObject[] frameDimension;
    public Sprite[] frames;
    private GameController gM;
    // Start is called before the first frame update
    void Start()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        for (int i = 0; i < artSpawns.Length; i++)
        {
            artSpawns[i].GetComponent<EmptyInspect>().position = i;
        }

        // ArtJson(jSon);
    }

    // Update is called once per frame
    public void LoadArt(int frameSize, int frame, int spawn, string url)
    {
        var loadedFrame = frames[frame];
        GameObject art = Instantiate(frameDimension[frameSize], new Vector3(0, 0, 0), Quaternion.identity);        
        art.transform.position = artSpawns[spawn].position;
        art.transform.rotation = Quaternion.Euler(0, artSpawns[spawn].eulerAngles.y + 90, 90);
        artSpawns[spawn].gameObject.SetActive(false);
        art.GetComponent<SpriteRenderer>().sprite = loadedFrame;
        StartCoroutine(GetTexture(url, art.transform.GetChild(0).gameObject));
    }
    IEnumerator GetTexture(string url, GameObject canvas)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();
        //
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            gM.totalItems += 1;
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            canvas.GetComponent<Renderer>().material.mainTexture = myTexture;
            canvas.GetComponent<Inspect>().url = url;
            gM.loadedItems += 1;
        }
    }
    void ArtJson(string json)
    {
        int frameSize = int.Parse(getBetween(json, "{\"size\":", ",\"frame"));
        int frame = int.Parse(getBetween(json, "frame\":", ",\"p"));
        int position = int.Parse(getBetween(json, "position\":", ",\"u"));
        string url = getBetween(json, "url\":\"", "\"}");
        LoadArt(frameSize, frame, position, url);
        Debug.Log(frameSize + " " + frame + " " + position + " " + url);
            
    }
    public static string getBetween(string strSource, string strStart, string strEnd)
    {
        if (strSource.Contains(strStart) && strSource.Contains(strEnd))
        {
            int Start, End;
            Start = strSource.IndexOf(strStart, 0) + strStart.Length;
            End = strSource.IndexOf(strEnd, Start);
            return strSource.Substring(Start, End - Start);
        }

        return "";
    }
}
