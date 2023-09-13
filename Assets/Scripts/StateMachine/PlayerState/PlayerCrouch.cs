using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : State
{
    public PlayerCrouch(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.Rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

        player.Rb.velocity = Vector2.zero;

        if (!isCrouching)
        {
            stateMachine.ChangeState(player.Idle);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
