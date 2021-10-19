using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtLoad : MonoBehaviour
{
    public string testURL;
    public int testFrame;
    public int testSpawn;
    public Transform[] artSpawns;
    public GameObject[] frames;
    // Start is called before the first frame update
    void Start()
    {
        LoadPictures(testFrame, testSpawn, testURL);
    }

    // Update is called once per frame
    public void LoadPictures(int frame, int spawn, string url)
    {
        GameObject art = Instantiate(frames[frame], new Vector3(0, 0, 0), Quaternion.identity);
        art.transform.position = artSpawns[spawn].position;
        art.transform.rotation = Quaternion.Euler(0, artSpawns[spawn].eulerAngles.y, 90);
        


        StartCoroutine(GetTexture(url, art.transform.GetChild(0).gameObject));
    }
    IEnumerator GetTexture(string url, GameObject canvas)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            canvas.GetComponent<Renderer>().material.mainTexture = myTexture;
        }
    }
}
