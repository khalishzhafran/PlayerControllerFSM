using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : State
{
    public PlayerIdle(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        if (horizontalInput != 0)
        {
            stateMachine.ChangeState(player.Run);
            return;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
