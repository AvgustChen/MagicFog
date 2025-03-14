using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }
    
    public event EventHandler JumpEvent;
    public event EventHandler AttackEvent;
    public event EventHandler WalkSoundEvent;
    public event EventHandler IdleSoundEvent;

    [SerializeField] private float walkSpeed = 1.5f; // максимальная скорость
    [SerializeField] private float runSpeed = 50f; // максимальная скорость
    [SerializeField] private float moveSpeed;
    [SerializeField] private float acceleration = 100f; // ускорение
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private KeyCode attackButton = KeyCode.Mouse0;
    [SerializeField] private float jumpForce = 5; // сила прыжка
    [SerializeField] private GameObject player;
    [SerializeField] private bool isGround;
    [SerializeField] private Transform followPoint;

    private Rigidbody body;
    private Vector3 direction;
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
        JumpEvent?.Invoke(this, EventArgs.Empty);
    }

    void Update()
    {
        if (Input.GetKeyDown(jumpButton) && isGround)
        {
            GetJump();
        }
        if (Input.GetKeyDown(attackButton))
        {
            AttackEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    void Move()
    {
        body.AddForce(direction.normalized * moveSpeed * acceleration * body.mass);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            player.transform.forward = direction.normalized;
        }
        direction = new Vector3(h, 0, v);
        direction = followPoint.transform.TransformDirection(direction);
        direction = new Vector3(direction.x, 0, direction.z);

        if (Mathf.Abs(body.velocity.x) > moveSpeed)
        {
            body.velocity = new Vector3(Mathf.Sign(body.velocity.x) * moveSpeed, body.velocity.y, body.velocity.z);
        }
        if (Mathf.Abs(body.velocity.z) > moveSpeed)
        {
            body.velocity = new Vector3(body.velocity.x, body.velocity.y, Mathf.Sign(body.velocity.z) * moveSpeed);
        }


        if (direction == Vector3.zero)
        {
            Idle();
        }
        else if (direction != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if (direction != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        IsWalking = true;
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        if (!IsWalking)
        {
            IsWalking = true;
            WalkSoundEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    void Idle()
    {
        moveSpeed = 0;
        if (IsWalking)
        {
            IsWalking = false;
            IdleSoundEvent?.Invoke(this, EventArgs.Empty);
        }

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