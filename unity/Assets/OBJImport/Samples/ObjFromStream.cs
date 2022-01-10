﻿using Dummiesman;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ObjFromStream : MonoBehaviour {
    private GameController gM;
    public Transform[] objSpawns;
    public string testUrl;
    public string testUrl2;
    public string testJson;
    public GameObject cameraPrefab;
   
    void Start () {
        //make www
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        for (int i = 0; i < objSpawns.Length; i++)
        {
            objSpawns[i].GetComponent<EmptyInspect>().position = i;
        }
        ObjJson(testJson);
    }
    
    public IEnumerator LoadObjs (int spawn, int texture, string url)
    {
       
            UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            //create stream and load
            gM.totalItems += 1;
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.downloadHandler.text));
            var loadedObj = new OBJLoader().Load(textStream);
            loadedObj.transform.position = objSpawns[spawn].position;
            for (int i = 0; i < loadedObj.transform.childCount; i++)
            {
                loadedObj.transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));
                loadedObj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                loadedObj.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = gM.textures[texture];
                loadedObj.transform.GetChild(i).gameObject.AddComponent<ObjSizing>();
            }
            for (int i = 0; i < gM.loadedObj.Count; i++)
            {
                if (gM.loadedObj[i].GetComponent<Inspect>().position == spawn)
                {
                    Destroy(gM.loadedObj[i].transform.parent.gameObject);
                    gM.loadedObj.RemoveAt(i);
                }
            }
            gM.loadedObj.Add(loadedObj.transform.GetChild(0).gameObject);
            var camera = Instantiate(cameraPrefab, new Vector3(loadedObj.transform.position.x + 4, loadedObj.transform.position.y - 2, loadedObj.transform.position.z), Quaternion.identity);
            objSpawns[spawn].gameObject.SetActive(false);
            camera.SetActive(false);
            camera.transform.parent = loadedObj.transform;
            loadedObj.transform.GetChild(0).gameObject.AddComponent<Inspect>().view = camera;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().url = url;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().texture = texture;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().position = spawn;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().DataCollectObj();
        }
       
    }
    public void ObjJson(string json)
    {
        /*  int position = int.Parse(getBetween(json, "{\"position\":", ",\"t"));
          int texture = int.Parse(getBetween(json, "{\"texture\":", ",\"u"));
          string url = getBetween(json, "url\":\"", "\"}");
          StartCoroutine(LoadObjs(position, texture, url));
          Debug.Log(position + " " + url);
        */
        string[] words = json.Split(',');
        int position = int.Parse(words[0]);
        int texture = int.Parse(words[1]);
        string url = words[2];
        StartCoroutine(LoadObjs(position, texture, url));
    }
   /* public static string getBetween(string strSource, string strStart, string strEnd)
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
   */
}
