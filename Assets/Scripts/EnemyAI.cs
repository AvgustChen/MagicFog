using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class EnemyAI : MonoBehaviour, ICanTakeDamage
{
    public event EventHandler DieEvent;
    public event EventHandler HitEvent;
    public event EventHandler AttackEvent;

    [SerializeField] private float healthMax;
    [SerializeField] private float level;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float impactForce;
    [SerializeField] private float radiusFindPlayer = 10;
    private Transform player;
    private float health;
    private bool isAttack;
    private bool canAttack;
    private bool canMove;
    private bool isDie;

    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject.transform;
        health = healthMax;
        canAttack = true;
    }
    private void Update()
    {

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > radiusFindPlayer)
        {
            Idle();
        }
        else if (distanceToPlayer <= attackRange)
        {
            // Нападаем на игрока
            if (!isAttack && canAttack)
            {
                Attack();
            }

        }
        else
        {
            // Бежим к игроку
            MoveTowardsPlayer();
        }

    }

    private void Idle()
    {

    }

    private void MoveTowardsPlayer()
    {
        transform.LookAt(player);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void Attack()
    {
        AttackEvent?.Invoke(this, EventArgs.Empty);

        isAttack = true;
        canMove = false;
        canAttack = false;
    }

    private void Die()
    {
        if (!isDie)
        {
            DieEvent?.Invoke(this, EventArgs.Empty);
            isDie = true;
            canAttack = false;
            canMove = false;
            Invoke("DestroyThis", 3f);
        }

    }

    private void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    public void Damage(int amountDamage)
    {
        GetComponent<EnemyUI>().GetHit(amountDamage);

        canMove = false;
        canAttack = false;
        health -= amountDamage;
        if (health < 0) health = 0;
        if (health == 0) Die();
    }

    public void CanMoveAttackSetTrue()
    {
        canMove = true;
        canAttack = true;
        isAttack = false;
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetHealthMax()
    {
        return healthMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && isAttack)
        {
            int rand = UnityEngine.Random.Range(0, (int)impactForce);
            Player.Instance.Damage(rand);
        }
    }


}