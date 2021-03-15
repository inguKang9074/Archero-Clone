using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    // Use this for initialization
    void Awake()
    {
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    public void SetIdle()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Run", false);
    }

    public void SetAttack()
    {
        animator.SetBool("Attack", true);
        animator.SetBool("Run", false);
    }

    public void SetRun()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Run", true);
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        animator.SetFloat("AttackSpeed", attackSpeed);
    }

    public void SetHitAnim()
    {
        animator.SetTrigger("Hit");
    }
}
