using UnityEngine;


public class EnemyAI : MonoBehaviour, ICanTakeDamage
{
    private float health;
    private float startHealth;
    private float speed = 3f;
    private float attackRange = 2f;
    private float impactForce;
    private float radiusFindPlayer = 10;
    private Transform player;
    bool isAttack;
    bool canAttack;
    public GameObject dieParticl;
    bool isDie;

    private void Start()
    {
        startHealth = health;
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

    }

    private void Die()
    {

    }

    public void Damage(int amountDamage)
    {
       
    }
}