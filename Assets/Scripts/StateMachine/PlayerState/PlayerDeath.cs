using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : State
{
    public PlayerDeath(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        isDead = true;
        player.Rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();

        player.Rb.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
