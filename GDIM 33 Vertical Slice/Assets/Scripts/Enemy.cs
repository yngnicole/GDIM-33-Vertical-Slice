using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Composites;

public class Enemy : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Transform _cat;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Stats")]
    [SerializeField] public int _maxHealth = 100;
    [SerializeField] private float _attackRange = 5f;
    [SerializeField] private float _attackCoolDown;

    [Header("Audio")]
    [SerializeField] private AudioSource _enemyAudioSource;
    [SerializeField] private AudioClip _enemyAudioClip;

    private float _lastAttackTime;
    private int _currentHealth;
    public int _damage = 10;
    private Color _originalColor;
    public float flashDuration = 0.1f;

    public static Action<int> OnEnemyTakeDamage;
    void Start()
    {
        Health();
        _originalColor = _spriteRenderer.color;
    }

    void FixedUpdate()
    {
        if (_cat == null) return;
        float distanceToCat = Vector2.Distance(transform.position, _cat.position);

        // If cat is close, attack
        if (distanceToCat < _attackRange)
        {
            Attack();
        }
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        StartCoroutine(FlashRed());

        if (_currentHealth <= 0)
        {
            Die();
        }

        OnEnemyTakeDamage?.Invoke(_currentHealth);
    }

    public void Attack()
    {
        if (_cat == null) return;

        if (Time.time - _lastAttackTime > _attackCoolDown)
        {
            Cat cat = _cat.GetComponent<Cat>();

            if (cat != null)
            {
                cat.TakeDamage(_damage);
            }


            _lastAttackTime = Time.time;
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Health()
    {
        _currentHealth = _maxHealth;

    }


    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.color = _originalColor;
    }

}

