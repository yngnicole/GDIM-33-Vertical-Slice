using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Rigidbody2D rb;
    //[SerializeField] private float _speed = 5f;
    //[SerializeField] private float _stopDistance = 1.5f;
    //[SerializeField] private float _attackRange = 2f;
    [SerializeField] public int _damage = 20;
    [SerializeField] public float _attackCooldown = 1f;
    [SerializeField] private int _maxHealth = 100;
    
    public float _currentHealth;
    private float lastAttackTime;

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
    }

    public void Attack()
    {
        // Stop movement
        //rb.velocity = Vector2.zero;

        if (_enemy == null) return;

        if(Time.time - lastAttackTime > _attackCooldown)
        {
           Enemy ghost = _enemy.GetComponent<Enemy>();
           
            if (ghost != null)
            {
                ghost.TakeDamage(_damage);
            }
           

            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
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
    }


    /*
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
