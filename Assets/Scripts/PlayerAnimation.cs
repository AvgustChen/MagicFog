using System;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string ISWALKING = "IsWalking";
    private const string ISJUMP = "Jump";
    private const string ATTACK = "Attack";
    [SerializeField] private GameObject player;
    private Animator animator;
    private void Awake()
    {
        animator = player.GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerControl.Instance.JumpEvent += PlayerControlJumpEvent;
        PlayerControl.Instance.AttackEvent += PlayerControlAttackEvent;
    }

    private void PlayerControlAttackEvent(object sender, EventArgs e)
    {
        int rand = UnityEngine.Random.Range(1, 5);
        string attack = ATTACK + rand.ToString();
        animator.SetTrigger(attack);
    }

    private void PlayerControlJumpEvent(object sender, EventArgs e)
    {
        animator.SetTrigger(ISJUMP);
    }

    private void Update()
    {
        animator.SetBool(ISWALKING, PlayerControl.Instance.GetIsWalking());
    }


}
