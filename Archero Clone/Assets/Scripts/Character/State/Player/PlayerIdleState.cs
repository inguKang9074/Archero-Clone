// 대기 상태
using UnityEngine;

public class PlayerIdleState : State<Player>
{
    private PlayerAnimation playerAnimation;
    private JoystickMove joystickMove;
    private Rigidbody rb;
    public override void OnEnter(Player target)
    {
        base.OnEnter(target);

        playerAnimation = character.playerAnimation;
        joystickMove = character.joystickMove;
        rb = character.rb;

        Debug.Log("IdleEnter");
    }

    public override void Update()
    {
        playerAnimation.SetIdle();
        Debug.Log("IdleUpdate");

        rb.velocity = Vector3.zero;

        Vector3 dir = joystickMove.GetJoystickDir();
        if (dir != Vector3.zero)
        {
            character.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        }   
    }

    public override void OnExit()
    {
        Debug.Log("IdleExit");
    }
}
