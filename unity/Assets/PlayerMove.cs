using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform head;
    public Transform player;
    public float speed;
    public float rotSpeed;
    private float currentSpeed;
    private float sprintSpeed;
    private Rigidbody rb;
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        player = this.transform;
        currentSpeed = speed;
        sprintSpeed = speed * 2;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotInput = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        Vector3 headInput = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);
        head.Rotate(headInput * Time.deltaTime * rotSpeed);
        player.Rotate(rotInput * Time.deltaTime * rotSpeed);
        if (Input.GetKey(KeyCode.W))
        {
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.MovePosition(transform.position - transform.forward * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.MovePosition(transform.position - transform.right * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.MovePosition(transform.position + transform.right * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed; 
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = speed;
        }
           
            
        
    }
}
