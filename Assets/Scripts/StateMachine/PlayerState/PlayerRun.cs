using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : State
{
    public PlayerRun(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        if (horizontalInput == 0)
        {
            stateMachine.ChangeState(player.Idle);
            return;
        }

        player.Rb.velocity = new Vector2(horizontalInput * player.Data.CurrentSpeed, player.Rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
