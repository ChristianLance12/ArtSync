using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSizingS : MonoBehaviour
{
    private float[] bounds = { 0, 0, 0 };
    private float max;
    private float scale;
    private bool done;
    private GameController gM;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ObjSize());
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();

    }
    // Update is called once per frame
    public IEnumerator ObjSize()
    {
        bounds[0] = GetComponent<MeshFilter>().mesh.bounds.size.x;
        bounds[1] = GetComponent<MeshFilter>().mesh.bounds.size.y;
        bounds[2] = GetComponent<MeshFilter>().mesh.bounds.size.z;

        for (int i = 1; i < bounds.Length; i++)
        {
            if (bounds[i] > max)
            {
                max = bounds[i];
            }

        }
        scale = 1 / max;
        transform.localScale = new Vector3(scale, scale, scale);
        yield return new WaitForSeconds(0.5f);
        gameObject.AddComponent(typeof(MeshCollider));
        
        gameObject.GetComponent<MeshCollider>().convex = true;
        gameObject.AddComponent<SphereCollider>();
        gameObject.GetComponent<SphereCollider>().radius = 2/scale;
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && done == false)
        {
            gM.loadedItems += 1;
            done = true;
        }
    }
}

