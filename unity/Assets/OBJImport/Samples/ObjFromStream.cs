using Dummiesman;
using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ObjFromStream : MonoBehaviour {
    private GameController gM;
    public Transform[] objSpawns;
    public GameObject cameraPrefab;
    public AudioSource placeSnd;
    void Awake()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    void Start () {
       
        for (int i = 0; i < objSpawns.Length; i++)
        {
            objSpawns[i].GetComponent<EmptyInspect>().position = i;
            
        }


    }
    
    public IEnumerator LoadObjs (int spawn, int texture, string url)
    {
        
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
            if (gM.viewing == true)
            {
                placeSnd.Play();
            }
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.downloadHandler.text));
            var loadedObj = new OBJLoader().Load(textStream);
            loadedObj.transform.position = objSpawns[spawn].position;
            for (int i = 0; i < loadedObj.transform.childCount; i++)
            {
                loadedObj.transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));
                loadedObj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                loadedObj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().mass = 100;
                loadedObj.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = gM.textures[texture];
                loadedObj.transform.GetChild(i).gameObject.AddComponent<ObjSizing>();
            }
            for (int i = 0; i < gM.loadedObjL.Count; i++)
            {
                if (gM.loadedObjL[i].GetComponent<Inspect>().position == spawn)
                {
                    Destroy(gM.loadedObjL[i].transform.parent.gameObject);
                    gM.loadedObjL.RemoveAt(i);
                    gM.loadedItems -= 1;
                    gM.totalItems -= 1;
                }
            }
            gM.loadedObjL.Add(loadedObj.transform.GetChild(0).gameObject);
            var camera = Instantiate(cameraPrefab, new Vector3(loadedObj.transform.position.x - 4, loadedObj.transform.position.y - 1.5f, loadedObj.transform.position.z), Quaternion.identity);
            objSpawns[spawn].gameObject.GetComponent<EmptyInspect>().view.SetActive(true);
            objSpawns[spawn].gameObject.SetActive(false);
            camera.SetActive(false);
            camera.transform.parent = loadedObj.transform;
            loadedObj.transform.GetChild(0).gameObject.AddComponent<Inspect>().view = camera;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().url = url;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().texture = texture;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().position = spawn;
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().DataCollectObj();
            loadedObj.transform.GetChild(0).gameObject.GetComponent<Inspect>().type = Type.LOBJ;
            loadedObj.transform.rotation = Quaternion.Euler(objSpawns[spawn].eulerAngles.x, objSpawns[spawn].eulerAngles.y + 90, objSpawns[spawn].eulerAngles.z);
            gM.selected = loadedObj.transform.GetChild(0).gameObject;
            gM.viewtxt.SetActive(true);
        }
       
    }
    public void DeleteObj(int position)
    {
        for (int i = 0; i < gM.loadedObjL.Count; i++)
        {
            if (gM.loadedObjL[i].GetComponent<Inspect>().position == position)
            {
                Destroy(gM.loadedObjL[i].transform.parent.gameObject);
                gM.loadedObjL.RemoveAt(i);
                gM.loadedItems -= 1;
                gM.totalItems -= 1;

            }
        }
        objSpawns[position].gameObject.SetActive(true);
        gM.selected = objSpawns[position].gameObject;
        gM.viewtxt2.SetActive(true);

    }
    public void ObjJson(string json)
    {
        gM.loadingDataObj.Add(json);
        gM.totalItems += 1;
    }
    public void ObjParse(string json)
    {
       
        string[] words = json.Split(',');
        int position = int.Parse(words[0]);
        int texture = int.Parse(words[1]);
        string url = words[2];
        StartCoroutine(LoadObjs(position, texture, url));
    }
    public void ObjDeleteJson(string position)
    {
        int positionI = int.Parse(position);
        DeleteObj(positionI);
    }
}
