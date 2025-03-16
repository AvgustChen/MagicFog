using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public event EventHandler AttackEvent;
    public event EventHandler TakeDamageEvent;
    public event EventHandler DieEvent;
    public event EventHandler WalkSoundEvent;
    public event EventHandler IdleSoundEvent;

    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float runSpeed;
    [SerializeField] private float acceleration = 100f;
    [SerializeField] private KeyCode attackButton = KeyCode.Mouse0;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform followPoint;


    private float moveSpeed;
    private Rigidbody body;
    private Vector3 direction;
    private float timerAttack;
    private float timerAttackStart = .7f;
    private bool IsWalking;
    private bool canMove;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        canMove = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        body = GetComponent<Rigidbody>();
        timerAttack = timerAttackStart;
    }

    void FixedUpdate()
    {
        if(canMove) Move();
    }


    void Update()
    {
        timerAttack -= Time.deltaTime;
        if (timerAttack <= 0)
        {
            canMove = true;
            if (Input.GetKeyDown(attackButton))
            {
                timerAttack = timerAttackStart;
                canMove = false;
                Attack();
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            int rand = UnityEngine.Random.Range(1, 21);
            Damage(rand);
        }
    }


    private void Move()
    {
        body.AddForce(direction * moveSpeed * acceleration * body.mass);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (h != 0 || v != 0)
        {
            player.transform.forward = direction;
        }
        direction = new Vector3(h, 0, v);
        direction = followPoint.transform.TransformDirection(direction.normalized);
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

    private void Idle()
    {
        moveSpeed = 0;
        if (IsWalking)
        {
            IsWalking = false;
            IdleSoundEvent?.Invoke(this, EventArgs.Empty);
        }

    }

    private void Attack()
    {
        AttackEvent?.Invoke(this, EventArgs.Empty);
    }

    public bool GetIsWalking()
    {
        return IsWalking;
    }

    public void Damage(int amountDamage)
    {
        TakeDamageEvent?.Invoke(this, EventArgs.Empty);
        PlayerData.Instance.TakeDamage(amountDamage);
    }

    public void LevelUp()
    {

    }

    public void Die()
    {
        DieEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter(Collider other)
    {
        var takeDamage = other.gameObject.GetComponent(typeof(ICanTakeDamage));
        if(takeDamage != null)
        {
            int rand = UnityEngine.Random.Range(0, (int)PlayerData.Instance.GetImpactForce());
            other.GetComponent<EnemyAI>().Damage(rand);
        }
    }
}