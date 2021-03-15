using UnityEngine;
using System.Collections;

public class PlayerRunState : State<Player>
{
    private PlayerAnimation playerAnimation;
    private JoystickMove joystickMove;
    private Rigidbody rb;

    public override void OnEnter(Player target)
    {
        base.OnEnter(target);
        Debug.Log("RunEnter");

        playerAnimation = character.playerAnimation;
        joystickMove = character.joystickMove;
        rb = character.rb;
    }

    public override void Update()
    {
        Debug.Log("RunUpdate");
        playerAnimation.SetRun();

        Vector3 dir = joystickMove.GetJoystickDir();
        if (dir != Vector3.zero)
        {
            character.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }
        rb.velocity = dir * character.Speed;

    }

    public override void OnExit()
    {
        Debug.Log("RunExit");
        rb.velocity = Vector3.zero;
    }

}
