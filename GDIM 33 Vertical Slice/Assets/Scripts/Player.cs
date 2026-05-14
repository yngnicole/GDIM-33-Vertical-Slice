using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed;
    [SerializeField] private int _maxHealth;

    private float flashDuration = 0.1f;
    private Color _originalColor;
    public int _currentHealth;

    public static Action<int> OnPlayerHeal;
    public static Action<int> OnPlayerTakeDamage;
    //public static Action<int> OnPlayerAttack;

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
    }

    private void Update()
    {
        //movement with WASD and arrow keys
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        UnityEngine.Vector2 movement = new UnityEngine.Vector2(movementX, movementY);

        transform.Translate(movement * _speed * Time.deltaTime);
    }

    private void Attack()
    {
        //press f key to attack 
        // check if enemy is within range to be able to attack 
    }

    private void TakeDamage(int damage)
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