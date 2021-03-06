using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSizing : MonoBehaviour
{
    private float[] bounds = { 0, 0, 0 };
    private float max;
    private float scale;
    private bool done;
    private GameController gM;
    // Start is called before the first frame update
    void Awake()
    {
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }
    void Start()
    {
        ObjSize();
    

    }
    // Update is called once per frame
    public void ObjSize()
    {
        gM.loading = false;
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
        scale = (float)2.5 / max;
        transform.localScale = new Vector3(scale, scale, scale);
        gameObject.AddComponent(typeof(MeshCollider));
        gameObject.GetComponent<MeshCollider>().convex = true;
        gameObject.AddComponent<SphereCollider>();
        gameObject.GetComponent<SphereCollider>().radius = 3/scale;
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
    void OnDrawGizmos()
    {
        // Draws a 5 unit long red line in front of the object
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5;
        Gizmos.DrawRay(transform.position, direction);
    }
}

