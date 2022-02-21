using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyObjCamera : MonoBehaviour
{
    public bool sObj;
    public GameObject pedestal;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sObj == true)
        {
          //  transform.LookAt(pedestal.transform);
            // transform.Translate(Vector3.right * Time.deltaTime * 0.65f);
        }
    }
}
