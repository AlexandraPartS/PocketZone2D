using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSMove : MonoBehaviour
{
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

    [SerializeField]
    private float speed = 5f;

    private void Awake()
    {
        playerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Mathf.RoundToInt(playerInput.Player.Move.ReadValue<Vector2>().x);
        var velocityX = speed * horizontal;
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }


}
