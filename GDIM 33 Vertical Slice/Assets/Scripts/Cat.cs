using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Stats")]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] public int _damage = 20;
    [SerializeField] public float _attackCooldown = 1f;

    [Header("Audio")]
    [SerializeField] private AudioSource _catAudioSource;
    [SerializeField] private AudioClip _catAudioClipTakeDmg;
    [SerializeField] private AudioClip _catAudioClipAttack;


    private int _currentHealth;
    private float _lastAttackTime;
    private Color _originalColor;
    private float flashDuration = 0.1f;

    public static Action<int> OnHeal;
    public static Action<int> OnTakeDamage;
    public static Action<int> OnPowerUp;
    private void OnEnable()
    {
        Items.OnConsumeMedicine += Heal;
        Items.OnConsumePowerUp += PowerUp;
    }

    private void OnDisable()
    {
        Items.OnConsumeMedicine -= Heal;
        Items.OnConsumePowerUp -= PowerUp;
    }
    void Start()
    {
        _currentHealth = _maxHealth;
        _originalColor = _spriteRenderer.color;

        OnHeal?.Invoke(_currentHealth);
        OnPowerUp?.Invoke(_damage);
    }

    public void Attack()
    {

        if (_enemy == null) return;

        if(Time.time - _lastAttackTime > _attackCooldown)
        {
           Enemy ghost = _enemy.GetComponent<Enemy>();
           
            if (ghost != null)
            {
                ghost.TakeDamage(_damage);
            }
           

            _lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        StartCoroutine(FlashRed());

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth <= 0)
        {
            Die();
        }

        OnTakeDamage?.Invoke(_currentHealth);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        OnHeal?.Invoke(_currentHealth);
    }
    
    public void PowerUp(int amount, float duration)
    {
        StartCoroutine(PowerUpRoutine(amount, duration));
    }

    private IEnumerator PowerUpRoutine(int amount, float duration)
    {
        _damage += amount;
        OnPowerUp?.Invoke(_damage);

        yield return new WaitForSeconds(duration);

        _damage -= amount;
        if (_damage <= 20) _damage = 20;
        OnPowerUp?.Invoke(_damage);
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(flashDuration);
        _spriteRenderer.color = _originalColor;
    }


    /* in state machine now 
    //[SerializeField] private float _speed = 5f;
    //[SerializeField] private float _stopDistance = 1.5f;
    //[SerializeField] private float _attackRange = 2f;

   void FixedUpdate()
   {
       if (_enemy == null) return;
       float distanceToEnemy = Vector2.Distance(transform.position, _enemy.position);

       // If enemy is close → attack
       if (distanceToEnemy < _attackRange)
       {
           Attack();
       }
       else
       {
           FollowPlayer();
       }
   }

   void FollowPlayer()
   {
       // Direction to player
       Vector2 direction = _player.position - transform.position;
       float distance = direction.magnitude;

       // Only move if not too close
       if (distance > _stopDistance)
       {
           rb.velocity = direction.normalized * _speed;
       }
       else
       {
           rb.velocity = Vector2.zero;
       }
   }
   */
}
