using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public System.Action OnDeath;

    private void OnEnable()
    {
        //Cat.OnAttack += TakeDamage;
    }

    private void OnDisable()
    {
       // Cat.OnAttack -= TakeDamage;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}

