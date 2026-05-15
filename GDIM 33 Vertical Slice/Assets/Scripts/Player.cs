using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _enemy;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Stats")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _attackDamage = 10;
    [SerializeField] private float _attackRange = 1.5f;
    [SerializeField] private float _attackCoolDown = 1f;
    //[SerializeField] private float _speed;

    [Header("Audio")]
    [SerializeField] private AudioSource _playerAudioSource;
    [SerializeField] private AudioClip _playerAudioClip;

    private float flashDuration = 0.1f;
    private Color _originalColor;
    private int _currentHealth;
    private float _lastAttackTime;


    public static Action<int> OnPlayerHeal;
    public static Action<int> OnPlayerTakeDamage;
    public static Action<int> OnPlayerAttack;

    private void OnEnable()
    {
        Items.OnConsumeMedicine += Heal;
    }

    private void OnDisable()
    {
        Items.OnConsumeMedicine -= Heal;
    }
    
    void Start()
    {
        _currentHealth = _maxHealth;
        _originalColor = _spriteRenderer.color;

        OnPlayerHeal?.Invoke(_currentHealth);
        OnPlayerAttack?.Invoke(_attackDamage);
    }

    private void Update()
    {
        //movement with WASD and arrow keys
        //float movementX = Input.GetAxisRaw("Horizontal");
        //float movementY = Input.GetAxisRaw("Vertical");

        //UnityEngine.Vector2 movement = new UnityEngine.Vector2(movementX, movementY);

        //transform.Translate(movement * _speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    private void Attack()
    {
        
        //fixme
        Collider2D hitEnemy = Physics2D.OverlapCircle(transform.position, _attackRange, _enemyLayer);


        if (hitEnemy == null) 
        {
            return;
        }
       
        if (Time.time - _lastAttackTime > _attackCoolDown)
        {
            Enemy ghost = hitEnemy.GetComponent<Enemy>();

            if (ghost != null)
            {
                ghost.TakeDamage(_attackDamage);
            }

            _lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        StartCoroutine(FlashRed());

        _playerAudioSource.PlayOneShot(_playerAudioClip);

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth <= 0)
        {
            Die();
        }

        OnPlayerTakeDamage?.Invoke(_currentHealth);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void Heal(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        OnPlayerHeal?.Invoke(_currentHealth);
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.color = _originalColor;
    }

}