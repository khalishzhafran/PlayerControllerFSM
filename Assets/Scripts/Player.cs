using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerData))]
[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }
    public PlayerIdle Idle { get; private set; }
    public PlayerRun Run { get; private set; }
    public PlayerCrouch Crouch { get; private set; }
    public PlayerHurt Hurt { get; private set; }
    public PlayerDeath Death { get; private set; }

    public Animator Animator { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public SpriteRenderer SpriteRenderer { get; private set; }

    public PlayerData Data { get; private set; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        SpriteRenderer = GetComponent<SpriteRenderer>();

        Data = GetComponent<PlayerData>();

        StateMachine = new StateMachine();
        Idle = new PlayerIdle(this, StateMachine, "Idle");
        Run = new PlayerRun(this, StateMachine, "Run");
        Crouch = new PlayerCrouch(this, StateMachine, "Crouch");
        Hurt = new PlayerHurt(this, StateMachine, "Hurt");
        Death = new PlayerDeath(this, StateMachine, "Death");

        StateMachine.Initialize(Idle);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StateMachine.CurrentState.OnColliderEnter2D(other);
    }

    public float GetHorizontalInput() => Input.GetAxisRaw("Horizontal");
    public bool GetCrouchInputHeld() => Input.GetKey(KeyCode.LeftControl);
    public bool GetShootInputHeld() => Input.GetKey(KeyCode.J);

    public void Flip()
    {
        Data.FacingRight *= -1;
        transform.Rotate(0f, 180f, 0f);
        Data.FlipBar();
    }

    public void ShootProjectile()
    {
        Data.ShootProjectile();
        Data.CurrentAmmo--;
    }

    public void ShowReloadBar(float current, float max)
    {
        Data.ShowBar();
        Data.ReloadBar.UpdateBar(current, max);
    }

    public void HideReloadBar()
    {
        Data.HideBar();
        Data.ReloadBar.UpdateBar(0, 1);
    }

    public void Flashing()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        SpriteRenderer.material = Data.HurtMaterial;
        yield return new WaitForSeconds(Data.HurtDuration);
        SpriteRenderer.material = Data.DefaultMaterial;
    }
}
