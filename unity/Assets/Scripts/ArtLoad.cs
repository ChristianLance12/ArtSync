using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArtLoad : MonoBehaviour
{
    public Transform[] artSpawns;
    public GameObject[] frameDimension;
    public Material[] frames;
    public AudioSource placeSnd;
    private GameController gM;
    // Start is called before the first frame update
    void Awake()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    void Start()
    {
        for (int i = 0; i < artSpawns.Length; i++)
        {
            artSpawns[i].GetComponent<EmptyInspect>().position = i;
           
        }


    }

    // Update is called once per frame
    public void LoadArt(int frameSize, int frame, int spawn, string url)
    {
       
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
        GameObject art = Instantiate(frameDimension[frameSize], new Vector3(0, 0, 0), Quaternion.identity);
        gM.loadedArt.Add(art.GetComponent<ContentFinder>().content);
        art.transform.position = artSpawns[spawn].position;
        art.transform.rotation = Quaternion.Euler(0, artSpawns[spawn].eulerAngles.y + 90, 90);
        artSpawns[spawn].gameObject.GetComponent<EmptyInspect>().view.SetActive(true);
        artSpawns[spawn].gameObject.SetActive(false);
        art.GetComponent<ContentFinder>().frame.GetComponent<MeshRenderer>().material = frames[frame];
        art.GetComponent<ContentFinder>().content.GetComponent<Inspect>().position = spawn;
        art.GetComponent<ContentFinder>().content.GetComponent<Inspect>().frameSize = frameSize;
        art.GetComponent<ContentFinder>().content.GetComponent<Inspect>().frame = frame;
        gM.selected = art.GetComponent<ContentFinder>().content;
        StartCoroutine(GetTexture(url, art.GetComponent<ContentFinder>().content));
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
            if (gM.viewing == true)
            {
                placeSnd.Play();
            }
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            canvas.GetComponent<Renderer>().material.mainTexture = myTexture;
            canvas.GetComponent<Inspect>().url = url;
            canvas.GetComponent<Inspect>().DataCollectArt();
            canvas.GetComponent<Inspect>().type = Type.ART;
            gM.loadedItems += 1;
            gM.loading = false;
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
        gM.loadingDataArt.Add(json);
        gM.totalItems += 1;
    }
    public void ArtParse(string json)
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
