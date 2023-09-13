using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Player player;
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected float stateTimer;
    protected float horizontalInput;
    protected bool isCrouching;
    protected bool isShooting;
    protected bool isReloading;
    protected bool isDead;
    protected int hurtCount;

    public State(Player player, StateMachine stateMachine, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        player.Animator.SetBool(animBoolName, true);
    }

    public virtual void Update()
    {
        if (isDead) return;

        horizontalInput = player.GetHorizontalInput();
        isCrouching = player.GetCrouchInputHeld();
        isShooting = player.GetShootInputHeld();

        HandleReload();
        HandleShooting();
        HandleCrouch();
        HandleFlip();
    }

    public virtual void OnColliderEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            stateMachine.ChangeState(player.Hurt);
        }
    }

    public virtual void Exit()
    {
        player.Animator.SetBool(animBoolName, false);
    }

    protected void HandleShooting()
    {
        if (isReloading) return;

        if (isShooting)
        {
            stateTimer -= Time.deltaTime;

            if (stateTimer <= 0)
            {
                player.ShootProjectile();
                stateTimer = player.Data.ShootDelay;

                if (player.Data.CurrentAmmo <= 0)
                {
                    isReloading = true;
                    stateTimer = 0;
                }
            }
        }
    }

    protected void HandleReload()
    {
        if (!isReloading) return;

        player.Data.CurrentSpeed = player.Data.ReloadSpeed;

        stateTimer += Time.deltaTime;

        player.ShowReloadBar(stateTimer, player.Data.ReloadTime);

        if (stateTimer >= player.Data.ReloadTime)
        {
            isReloading = false;
            stateTimer = 0;
            player.Data.CurrentAmmo = player.Data.MaxAmmo;
            player.Data.CurrentSpeed = player.Data.Speed;
            player.HideReloadBar();
        }
    }

    protected void HandleFlip()
    {
        if (horizontalInput < 0 && player.Data.FacingRight == 1)
        {
            player.Flip();
        }
        else if (horizontalInput > 0 && player.Data.FacingRight == -1)
        {
            player.Flip();
        }
    }

    protected void HandleCrouch()
    {
        if (isCrouching)
        {
            stateMachine.ChangeState(player.Crouch);
            return;
        }
    }
}
