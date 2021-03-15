using UnityEngine;
using System.Collections;

public class PlayerHitState : State<Player>
{
    private Rigidbody rb;
    private PlayerAnimation playerAnimation;
    private JoystickMove joystickMove;

    public override void OnEnter(Player target)
    {
        base.OnEnter(target);
        Debug.Log("HitEnter");
        playerAnimation = character.playerAnimation;
        rb = character.rb;
        joystickMove = character.joystickMove;
        playerAnimation.SetHitAnim();
    }

    public override void Update()
    {
        Debug.Log("HitUpdate");

        rb.velocity = joystickMove.GetJoystickDir() * character.Speed * 0.2f;
    }

    public override void OnExit()
    {
        Debug.Log("HitExit");
    }

}
