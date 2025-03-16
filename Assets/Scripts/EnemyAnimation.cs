using System;
using System.Collections;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private const string DIE = "Die";
    private const string HIT = "GetHit";
    private Animator animator;
    private EnemyAI enemyAI;
    [SerializeField] private int countAttacks;
    [SerializeField] private GameObject dieParticl;

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        enemyAI.DieEvent += EnemyAI_DieEvent;
        enemyAI.HitEvent += EnemyAI_HitEvent;
        enemyAI.AttackEvent += EnemyAI_AttackEvent;
    }

    private void EnemyAI_AttackEvent(object sender, EventArgs e)
    {
        int rand = UnityEngine.Random.Range(1, countAttacks + 1);
        string attack = "Attack" + rand;
        animator.SetTrigger(attack);
        StartCoroutine(SetTrueCanMoveAttack());
    }

    private void EnemyAI_HitEvent(object sender, EventArgs e)
    {
        animator.SetTrigger(HIT);
        StartCoroutine(SetTrueCanMoveAttack());
    }

    private void EnemyAI_DieEvent(object sender, EventArgs e)
    {
        animator.SetTrigger(DIE);
        if (!dieParticl.activeInHierarchy)
            dieParticl.SetActive(true);
    }

    private IEnumerator SetTrueCanMoveAttack()
    {
        yield return new WaitForSeconds(.7f);
        enemyAI.CanMoveAttackSetTrue();
    }

}
