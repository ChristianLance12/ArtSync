using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSizing : MonoBehaviour
{
    private float[] bounds = { 0, 0, 0 };
    private float max;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        ObjSize();
    }
    // Update is called once per frame
    public void ObjSize()
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
        scale = 4 / max;
        transform.localScale = new Vector3(scale, scale, scale);
        gameObject.AddComponent(typeof(MeshCollider));
       gameObject.GetComponent<MeshCollider>().convex = true;
        gameObject.AddComponent<SphereCollider>();
        gameObject.GetComponent<SphereCollider>().radius = 5/scale;
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
    }
}

