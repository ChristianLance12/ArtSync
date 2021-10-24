using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform head;
    private Transform player;
    private Vector3 newRot;
    public float speed;
    public float rotY;
    public float rotX;
    public float sprintMultiplier;
    private float currentSpeed;
    private float sprintSpeed;
    private Rigidbody rb;
    private GameController gM;
    void Start()
    {
        
        rb = this.GetComponent<Rigidbody>();
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        player = this.transform;
        currentSpeed = speed;
        sprintSpeed = speed * sprintMultiplier;
        head.localRotation = Quaternion.Euler(0, 0, 0);


    }

    // Update is called once per frame
    void Update()
    {
        if (gM.viewing == false)
        {
            Vector3 rotInput = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            Vector3 headInput = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);
            head.Rotate(headInput * Time.deltaTime * rotY);
            player.Rotate(rotInput * Time.deltaTime * rotX);
            var v3 = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
            transform.Translate(currentSpeed * v3.normalized * Time.deltaTime);
            if (head.localRotation.x > 0.6)
            {
                head.localRotation = Quaternion.Euler(75f, 0, 0);
            }
            else if (head.localRotation.x < -0.6)
            {
                head.localRotation = Quaternion.Euler(-75f, 0, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                v3 += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                v3 += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A))
            {
                v3 += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                v3 += Vector3.right;
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
}
