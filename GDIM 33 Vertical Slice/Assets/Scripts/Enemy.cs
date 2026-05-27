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
    [SerializeField] private int _damage = 10;
    private float _lastAttackTime;
    private int _currentHealth;

    [Header("Audio")]
    [SerializeField] private AudioSource _enemyAudioSource;
    [SerializeField] private AudioClip _enemyAudioClip;

    [Header("Movement")]
    [SerializeField] private GameObject _pointA;
    [SerializeField] private GameObject _pointB;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private Transform _currentPoint;
    private bool _isMovementFreeze = false;


    private Color _originalColor;
    private float flashDuration = 0.1f;

    public static Action<int> OnEnemyTakeDamage;
    void Start()
    {
        Health();
        _originalColor = _spriteRenderer.color;
        
        OnEnemyTakeDamage?.Invoke(_currentHealth);

       _currentPoint = _pointB.transform;
    }

    void FixedUpdate()
    {

        if (!_isMovementFreeze)
        {
            HandleMovement();
        }

        CheckForTargets();
    }

    public void HandleMovement()
    {
        if (_currentPoint == null || _rb == null) return;

        if (_currentPoint.position.y > transform.position.y)
        {
            _rb.velocity = new Vector2(0, _speed);
        }
        else
        {
            _rb.velocity = new Vector2(0, -_speed);
        }

        float yDistance = Mathf.Abs(transform.position.y - _currentPoint.position.y);

        if (yDistance < 0.5f)
        {
            if (_currentPoint == _pointB.transform)
            {
                _currentPoint = _pointA.transform;
            }
            else if (_currentPoint == _pointA.transform)
            {
                _currentPoint = _pointB.transform;
            }
        }
    }

    private void CheckForTargets()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _attackRange);

        bool attackedSomeone = false;

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Cat"))
            {
                Cat cat = collider.GetComponent<Cat>();
                if (cat != null)
                {
                    if (Time.time - _lastAttackTime > _attackCoolDown)
                    {
                        cat.TakeDamage(_damage);
                        attackedSomeone = true;
                    }
                }
            }

            if (collider.CompareTag("Player"))
            {
                Player player = collider.GetComponent<Player>();
                if (player != null)
                {
                    if (Time.time - _lastAttackTime > _attackCoolDown)
                    {
                        player.TakeDamage(_damage);
                        attackedSomeone = true;
                    }
                }
            }
        }


        if (attackedSomeone)
        {
            _lastAttackTime = Time.time;
            FreezePermanentlyOnAttack();
        }
    }

    private void FreezePermanentlyOnAttack()
    {
        _isMovementFreeze = true;

        if (_rb != null)
        {
            _rb.velocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Static; 
        }
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;   
        StartCoroutine(FlashRedAndFreezeMovement());

        _enemyAudioSource.PlayOneShot(_enemyAudioClip);

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            
        }

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

    private IEnumerator FlashRedAndFreezeMovement()
    {
        _isMovementFreeze = true;

        if (_rb != null)
        {
            _rb.velocity = Vector2.zero;
            _rb.bodyType = RigidbodyType2D.Static;
        }

        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = Color.red;
        }

        yield return new WaitForSeconds(flashDuration);

        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _originalColor;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(_pointB.transform.position, 0.5f);
        Gizmos.DrawLine(_pointA.transform.position, _pointB.transform.position);
    }
}

