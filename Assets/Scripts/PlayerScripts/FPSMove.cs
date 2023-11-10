using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSMove : MonoBehaviour
{
    private bool facingRight = true;
    //public float speed = 4.0f;
    //public float jumpforce = 1.0f;
    //public Rigidbody2D PlayerRigidbody;
    //public SpriteRenderer sprite;

    //private void Update()
    //{
    //    if (Input.GetButton("Horizontal"))
    //    {
    //        Move();
    //    }
    //    //if(OnGround)
    //}




    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Transform tr;
    private int horizontal;

    [SerializeField]
    private float speed = 5f;

    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Mathf.RoundToInt(playerInput.Player.Move.ReadValue<Vector2>().x);
        var velocityX = speed * horizontal;
        rb.velocity = new Vector2(velocityX, rb.velocity.y);

        var vertical = Mathf.RoundToInt(playerInput.Player.Move.ReadValue<Vector2>().y);
        var velocityY = speed * vertical;
        rb.velocity = new Vector2(rb.velocity.x, velocityY);
    }

    private void FixedUpdate()
    {
        if(facingRight == false && horizontal > 0)
        {
            Flip();
        }
        else if(facingRight == true &&  horizontal < 0) 
        {
            Flip();
        }
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = tr.localScale;
        scaler.x *= -1;
        tr.localScale = scaler;
    }
}
