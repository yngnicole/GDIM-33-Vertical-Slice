using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _enemy;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _stopDistance = 1.5f;
    [SerializeField] private float _attackRange = 2f;
    [SerializeField] public int damage = 20;
    public float attackCooldown = 1f;
    private float lastAttackTime;


    void FixedUpdate()
    {
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

    void Attack()
    {
        // Stop movement
        rb.velocity = Vector2.zero;

        if(Time.time - lastAttackTime > attackCooldown)
    {
            Enemy ghost = _enemy.GetComponent<Enemy>();

            if (ghost != null)
            {
                ghost.TakeDamage(damage);
            }

            lastAttackTime = Time.time;
        }
    }
}
