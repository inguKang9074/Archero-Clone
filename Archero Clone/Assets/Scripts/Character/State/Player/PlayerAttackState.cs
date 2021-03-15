using UnityEngine;
using System.Collections;

public class PlayerAttackState : State<Player>
{
    private PlayerAnimation playerAnimation;
    private GameObject enemy;
    private Rigidbody rb;

    public override void OnEnter(Player target)
    {
        base.OnEnter(target);
        Debug.Log("AttackEnter");
        playerAnimation = character.playerAnimation;
        rb = character.rb;

        playerAnimation.SetAttack();
    }

    public override void Update()
    {
        Debug.Log("AttackUpdate");
        GameObject targer = character.GetTarget();

        character.transform.rotation = Quaternion.LookRotation(
            targer.transform.position - character.transform.position, 
            Vector3.up);
        rb.velocity = Vector3.zero;
    }

    public override void OnExit()
    {
        Debug.Log("AttackExit");
    }
}
