using Dummiesman;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SmallObjFromStream : MonoBehaviour {
    private GameController gM;
    public Transform[] objSpawns;
    public GameObject cameraPrefab;
   
    void Start () {
        //make www
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        for (int i = 0; i < objSpawns.Length; i++)
        {
            objSpawns[i].GetComponent<EmptyInspect>().position = i;
            objSpawns[i].GetComponent<EmptyInspect>().positionS = i.ToString();
        }


    }
    
    public IEnumerator LoadObjs (int spawn, int texture, string url)
    {
        gM.totalItems += 1;
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            gM.totalItems -= 1;
        }
        else
        {
            //create stream and load
        
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.downloadHandler.text));
            var loadedObj = new OBJLoader().Load(textStream);
            loadedObj.transform.position = objSpawns[spawn].position;
            for (int i = 0; i < loadedObj.transform.childCount; i++)
            {
                loadedObj.transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));
                loadedObj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                loadedObj.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = gM.textures[texture];
                loadedObj.transform.GetChild(i).gameObject.AddComponent<ObjSizingS>();
            }
            for (int i = 0; i < gM.loadedObjS.Count; i++)
            {
                if (gM.loadedObjS[i].GetComponent<Inspect>().position == spawn)
                {
                    Destroy(gM.loadedObjS[i].transform.parent.gameObject);
                    gM.loadedObjS.RemoveAt(i);
                    gM.loadedItems -= 1;
                    gM.totalItems -= 1;
                }
            }
            gM.loadedObjS.Add(loadedObj.transform.GetChild(0).gameObject);
            var camera = Instantiate(cameraPrefab, new Vector3(loadedObj.transform.position.x + 4, loadedObj.transform.position.y - 2, loadedObj.transform.position.z), Quaternion.identity);
            objSpawns[spawn].GetComponent<EmptyInspect>().enabled = false;
            objSpawns[spawn].GetComponent<EmptyInspect>().view.SetActive(false);
            camera.SetActive(false);
            camera.transform.parent = loadedObj.transform;
            
            loadedObj.transform.GetChild(0).gameObject.AddComponent<Inspect>().view = camera;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().url = url;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().texture = texture;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().position = spawn;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().DataCollectObj();
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().type = Type.SOBJ;
        }
       
    }
    public void ObjJson(string json)
    {
        
        string[] words = json.Split(',');
        int position = int.Parse(words[0]);
        int texture = int.Parse(words[1]);
        string url = words[2];
        StartCoroutine(LoadObjs(position, texture, url));
    }
  
}
