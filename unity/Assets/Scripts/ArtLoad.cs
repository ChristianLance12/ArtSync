using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtLoad : MonoBehaviour
{
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


    }

    // Update is called once per frame
    public void LoadArt(int frameSize, int frame, int spawn, string url)
    {
        gM.totalItems += 1;
        for (int i = 0; i < gM.loadedArt.Count; i++)
        {
            if (gM.loadedArt[i].GetComponent<Inspect>().position == spawn)
            {
                Destroy(gM.loadedArt[i].transform.parent.gameObject);
                gM.loadedArt.RemoveAt(i);
                gM.loadedItems -= 1;
                gM.totalItems -= 1;
                
            }
        }        
        var loadedFrame = frames[frame];
        GameObject art = Instantiate(frameDimension[frameSize], new Vector3(0, 0, 0), Quaternion.identity);
        gM.loadedArt.Add(art.transform.GetChild(0).gameObject);
        art.transform.position = artSpawns[spawn].position;
        art.transform.rotation = Quaternion.Euler(0, artSpawns[spawn].eulerAngles.y + 90, 90);
        
        artSpawns[spawn].gameObject.SetActive(false);
        art.GetComponent<SpriteRenderer>().sprite = loadedFrame;
        art.transform.GetChild(0).gameObject.GetComponent<Inspect>().position = spawn;
        art.transform.GetChild(0).gameObject.GetComponent<Inspect>().frameSize = frameSize;
        art.transform.GetChild(0).gameObject.GetComponent<Inspect>().frame = frame;
        gM.selected = art.transform.GetChild(0).gameObject;
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
            gM.totalItems -= 1;
        }
        else
        {
            
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            canvas.GetComponent<Renderer>().material.mainTexture = myTexture;
            canvas.GetComponent<Inspect>().url = url;
            canvas.GetComponent<Inspect>().DataCollectArt();
            canvas.GetComponent<Inspect>().type = Type.ART;
            gM.loadedItems += 1;
            gM.viewtxt.SetActive(true);
        }
    }
    public void DeleteArt(int position)
    {
        for (int i = 0; i < gM.loadedArt.Count; i++)
        {
            if (gM.loadedArt[i].GetComponent<Inspect>().position == position)
            {
                Destroy(gM.loadedArt[i].transform.parent.gameObject);
                gM.loadedArt.RemoveAt(i);
                gM.loadedItems -= 1;
                gM.totalItems -= 1;

            }
        }
        artSpawns[position].gameObject.SetActive(true);
        gM.selected = artSpawns[position].gameObject;
        gM.viewtxt2.SetActive(true);
    }
    public void ArtJson(string json)
    {
       
        string[] words = json.Split(',');
        int frameSize = int.Parse(words[0]);
        int frame = int.Parse(words[1]);
        int position = int.Parse(words[2]);
        string url = words[3];
        LoadArt(frameSize, frame, position, url);
      
            
    }
    public void ArtDeleteJson(string position)
    {
       int positionI = int.Parse(position);
        DeleteArt(positionI);
    }
}
