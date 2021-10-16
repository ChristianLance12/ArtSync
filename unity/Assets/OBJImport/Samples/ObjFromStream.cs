using Dummiesman;
using System.IO;
using System.Text;
using UnityEngine;

public class ObjFromStream : MonoBehaviour {
    public Transform[] spawnPoints;
	void Start () {
        //make www
        var www = new WWW("https://people.sc.fsu.edu/~jburkardt/data/obj/gourd.obj");
        while (!www.isDone)
            System.Threading.Thread.Sleep(1);
        
        //create stream and load
        var textStream = new MemoryStream(Encoding.UTF8.GetBytes(www.text));
        var loadedObj = new OBJLoader().Load(textStream);
        loadedObj.transform.position = spawnPoints[0].position;
        for (int i = 0; i < loadedObj.transform.childCount; i++)
        {
            loadedObj.transform.GetChild(i).gameObject.AddComponent(typeof(MeshCollider));
            loadedObj.transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody));
            loadedObj.transform.GetChild(i).gameObject.GetComponent<MeshCollider>().convex = true;
            loadedObj.transform.GetChild(i).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        
	}
}
