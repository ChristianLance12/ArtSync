using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine;

public class ObjFromStream : MonoBehaviour {
    private GameController gM;
    public Transform[] objSpawns;
    public string testUrl;
    public string testUrl2;
    public int testSpawn;
    public int testSpawn2;
    //https://people.sc.fsu.edu/~jburkardt/data/obj/gourd.obj
    void Start () {
        //make www
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        LoadObjs(testSpawn, testUrl);
        LoadObjs(testSpawn2, testUrl2);
    }
    
    public void LoadObjs (int spawn, string url)
    {
       
            var www = new WWW(url);
            while (!www.isDone)
                System.Threading.Thread.Sleep(1);

            //create stream and load
            var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
            var loadedObj = new OBJLoader().Load(textStream);
            loadedObj.transform.position = objSpawns[spawn].position;
            for (int i = 0; i < loadedObj.transform.childCount; i++)
            {
                loadedObj.transform.GetChild(i).gameObject.AddComponent(typeof(MeshCollider));
                loadedObj.transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));
                loadedObj.transform.GetChild(i).gameObject.GetComponent<MeshCollider>().convex = true;
                loadedObj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
                loadedObj.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material = gM.marble;
            loadedObj.transform.GetChild(i).gameObject.AddComponent<ObjSizing>();
            }
          
        
	}
}
