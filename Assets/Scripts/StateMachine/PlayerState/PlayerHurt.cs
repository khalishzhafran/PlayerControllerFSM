using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurt : State
{
    public PlayerHurt(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

        hurtCount++;
        player.Flashing();

        if (hurtCount >= 5)
        {
            stateMachine.ChangeState(player.Death);
            return;
        }

        stateMachine.ChangeState(player.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
