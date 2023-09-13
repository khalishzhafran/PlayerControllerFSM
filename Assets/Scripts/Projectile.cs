using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[DisallowMultipleComponent]
public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float lifeTime = 3f;
    private float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void Initialize(float speed)
    {
        this.speed = speed;
    }
}
