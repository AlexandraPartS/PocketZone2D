using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseObject 

{
    public float speed = 0.0f;
    //public float rotationSpeed;
    //public float verticalInput;

    //private float speed = 20.0f;
    //private float turnSpeed = 45.0f;
    private float turnSpeed = 0.0f;
    private float horizontalInput;
    private float forwardInput;


    void Start()
    {
        
    }
    void FixedUpdate()
    {
        //verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //transform.Rotate(Vector3.left * verticalInput * rotationSpeed * Time.deltaTime);
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //Moves the vehicle forward based on vertical input
        //transform.Translate(0, 0, 1);
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        //Rotates the car based on horizontal input & axis Y
        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * horizontalInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

    }

    void Update()
    {
        
    }
}
