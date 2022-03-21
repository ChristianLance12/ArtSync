using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Transform head;
    private Transform player;
    private Vector3 newRot;
    public bool moving;
    private bool sprinting;
    public AudioSource footSteps1;
    public AudioSource footSteps2;
    public float speed;
    public float rotY;
    public float newRotY;
    public float rotX;
    public float newRotX;
    public float sprintMultiplier;
    private float currentSpeed;
    private float sprintSpeed;
    private Rigidbody rb;
    private GameController gM;
    private Vector3 lastLoc;
    public float direction;
    void Start()
    {
        
        rb = this.GetComponent<Rigidbody>();
        gM = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        player = this.transform;
        currentSpeed = speed;
        sprintSpeed = speed * sprintMultiplier;
        head.localRotation = Quaternion.Euler(0, 0, 0);
        newRotY = rotY;
        newRotX = rotX;
        /*
        footSteps1.Pause();
        footSteps2.Pause();
        */
        lastLoc = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void Update()
    {
        if (gM.viewing == false && gM.loadingScreenOn == false && gM.paused == false)
        {
            Vector3 rotInput = new Vector3(0, Input.GetAxis("Mouse X"), 0);
            Vector3 headInput = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);
            head.Rotate(headInput * Time.deltaTime * newRotY);
            player.Rotate(rotInput * Time.deltaTime * newRotX);
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
                sprinting = true;
                currentSpeed = sprintSpeed;
                if (moving == true)
                {
                    footSteps1.Stop();
                    footSteps2.Play();
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {

                sprinting = false;
                currentSpeed = speed;
                if (moving == true)
                {
                    footSteps1.Play();
                    footSteps2.Stop();
                }
            }
                /*
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    moving = true;
                }
                else
                {
                    moving = false;
                }
                */
                if (direction > 0.0001 && moving == false && gM.introLoad == false)
            {
                if (sprinting == false)
                {
                    footSteps1.Play();
                }
                else if (sprinting == true)
                {
                    footSteps2.Play();
                }
                moving = true;
            }
            else if (direction < 0.0001 && moving == true)
            {
                footSteps1.Stop();
                footSteps2.Stop();
                moving = false;
            }

            /*
           if (gM.paused == false && gM.viewing == false)
           {

               if (sprinting == false)
               {
                   if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                   {
                       footSteps1.UnPause();
                   }
               }
               else if (sprinting == true)
               {
                   if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
                   {
                       footSteps2.UnPause();
                   }
               }
               if (moving == false)
               {
                   if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
                   {
                       footSteps1.Pause();
                   }
               }

               if (Input.GetKeyDown(KeyCode.LeftShift))
               {
                   sprinting = true;
                   currentSpeed = sprintSpeed;
                   if (moving == true)
                   {
                       footSteps1.Pause();
                       footSteps2.UnPause();
                   }
               }
               if (Input.GetKeyUp(KeyCode.LeftShift))
               {

                   sprinting = false;
                   currentSpeed = speed;
                   if (moving == true)
                   {
                       footSteps1.UnPause();
                       footSteps2.Pause();
                   }
                   else
                   {
                       footSteps1.Pause();
                       footSteps2.Pause();
                   }
               }
               if (sprinting == true && moving == false)
               {

                   footSteps2.Pause();
               }
               

        }
            */
        }

    }
    void FixedUpdate()
    {
        Vector3 newLoc = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        direction = Vector3.Distance(lastLoc, newLoc);
        lastLoc = newLoc;
    }
}
