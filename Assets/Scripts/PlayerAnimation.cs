using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string ISWALKING = "IsWalking";
    [SerializeField] private GameObject player;
    private Animator animator;
    private void Awake()
    {
        animator = player.GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(ISWALKING, PlayerControl.Instance.GetIsWalking());
    }


}
