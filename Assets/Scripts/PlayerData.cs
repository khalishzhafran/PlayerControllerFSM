using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    public float Speed { get { return speed; } }
    public float CurrentSpeed { get; set; }
    public float ReloadSpeed { get { return speed / 2; } }
    [SerializeField] private float hurtDuration = 0.15f;
    public float HurtDuration { get { return hurtDuration; } }
    [SerializeField] private Material hurtMaterial;
    public Material HurtMaterial { get { return hurtMaterial; } }
    public Material DefaultMaterial { get; private set; }

    [SerializeField] private float shootDelay = 0.5f;
    public float ShootDelay { get { return shootDelay; } }
    [SerializeField] private int maxAmmo = 10;
    public int MaxAmmo { get { return maxAmmo; } }
    public int CurrentAmmo { get; set; }
    [SerializeField] private float reloadTime = 1f;
    public float ReloadTime { get { return reloadTime; } }

    [Header("Projectile")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;

    [Header("UI")]
    [SerializeField] private Bar reloadBar;
    public Bar ReloadBar { get { return reloadBar; } }
    [SerializeField] private CanvasGroup barCanvas;

    public float FacingRight { get; set; } = 1;

    private void Awake()
    {
        CurrentAmmo = maxAmmo;
        CurrentSpeed = speed;

        DefaultMaterial = GetComponent<SpriteRenderer>().material;
    }

    public void ShootProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        projectile.Initialize(projectileSpeed * FacingRight);
    }

    public void ShowBar()
    {
        barCanvas.alpha = 1;
        barCanvas.blocksRaycasts = true;
        barCanvas.interactable = true;
    }

    public void HideBar()
    {
        barCanvas.alpha = 0;
        barCanvas.blocksRaycasts = false;
        barCanvas.interactable = false;
    }

    public void FlipBar()
    {
        barCanvas.transform.Rotate(0f, 180f, 0f);
    }
}
