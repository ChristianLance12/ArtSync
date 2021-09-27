using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameObject head;
    public Vector3 PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
       PlayerPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            this.Transform.forward;
        }
            }
}
