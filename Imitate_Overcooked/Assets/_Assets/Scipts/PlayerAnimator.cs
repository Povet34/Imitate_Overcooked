using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    const string IsWalking = "IsWalking";

    [SerializeField] Player player;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IsWalking, player.IsMoving());
    }
}
