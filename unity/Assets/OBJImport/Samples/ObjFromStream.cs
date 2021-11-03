using Dummiesman;
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
    public int testSpawn;
    public int testSpawn2;
    public GameObject cameraPrefab;
   
    void Start () {
        //make www
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
       StartCoroutine(LoadObjs(testSpawn, testUrl));
      StartCoroutine(LoadObjs(testSpawn2, testUrl2));
    }
    
    public IEnumerator LoadObjs (int spawn, string url)
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
                loadedObj.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = gM.marble;
                loadedObj.transform.GetChild(i).gameObject.AddComponent<ObjSizing>();
            }
            var camera = Instantiate(cameraPrefab, new Vector3(loadedObj.transform.position.x + 6, loadedObj.transform.position.y - 4, loadedObj.transform.position.z), Quaternion.identity);
            camera.SetActive(false);
            camera.transform.parent = loadedObj.transform;
            loadedObj.transform.GetChild(0).gameObject.AddComponent<Inspect>().view = camera;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().url = url;
        }
       
    }
    void ObjJson(string json)
    {
        int position = int.Parse(getBetween(json, "{\"position\":", ",\"u"));
        string url = getBetween(json, "url\":\"", "\"}");
        StartCoroutine(LoadObjs(position, url));
        Debug.Log(position + " " + url);

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
