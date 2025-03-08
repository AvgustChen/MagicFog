using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance {get; private set;}
    public event EventHandler AnimatorEvent;
    [SerializeField] private float walkSpeed = 1.5f; // макс. скорость
    [SerializeField] private float runSpeed = 50f; // макс. скорость
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration = 100f; // ускорение
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private float jumpForce = 10; // сила прыжка
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject followPoint;
    [SerializeField] private bool isGround;
    private Vector3 direction;
    private float h, v;
    private Rigidbody body;
    private bool IsWalking;

    void Awake()
    {
        Instance = this;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isGround = true;
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void GetJump()
    {
        body.velocity = new Vector2(0, jumpForce);
    }

    void Update()
    {

        if (Input.GetKeyDown(jumpButton))
        {
            if (isGround)
                GetJump();
        }
    }
    void Move()
    {
        body.AddForce(direction.normalized * moveSpeed * acceleration * body.mass);

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            player.transform.forward = direction;
        }


        direction = new Vector3(h, 0, v);
        direction = Camera.main.transform.TransformDirection(direction);
        direction = new Vector3(direction.x, 0, direction.z);


        if (Mathf.Abs(body.velocity.x) > moveSpeed)
        {
            body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * moveSpeed, body.velocity.y, body.velocity.z);
        }
        if (Mathf.Abs(body.velocity.z) > moveSpeed)
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * moveSpeed);
        }


        if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if (direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else if (direction == Vector3.zero)
        {
            Idle();
        }


    }
    void Idle()
    {
        moveSpeed = 0;
        IsWalking = false;
    }

    void Walk()
    {
            moveSpeed = walkSpeed;
            IsWalking = true;
    }

    void Run()
    {
            moveSpeed = runSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGround = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGround = false;
        }
    }

    public bool GetIsWalking()
    {
        return IsWalking;
    }
}