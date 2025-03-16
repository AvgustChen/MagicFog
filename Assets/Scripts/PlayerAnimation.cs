using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string ISWALKING = "IsWalking";
    private const string ATTACK = "Attack";
    private const string DIE = "Die";

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject swordFX;
    private Animator animator;
    private void Awake()
    {
        animator = player.GetComponent<Animator>();
    }

    private void Start()
    {
        Player.Instance.AttackEvent += PlayerControlAttackEvent;
        Player.Instance.DieEvent += PlayerControlDieEvent;
    }

    private void Update()
    {
        animator.SetBool(ISWALKING, Player.Instance.GetIsWalking());
    }

    private void PlayerControlDieEvent(object sender, EventArgs e)
    {
        animator.SetTrigger(DIE);
    }

    private void PlayerControlAttackEvent(object sender, EventArgs e)
    {
        int rand = UnityEngine.Random.Range(1, 5);
        string attack = ATTACK + rand.ToString();
        animator.SetTrigger(attack);
        swordFX.SetActive(true);
    }

    


}
