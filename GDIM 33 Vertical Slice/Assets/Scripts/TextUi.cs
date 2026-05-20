using System.Collections;
using System.Collections.Generic;
using TMPro;
//using TMPro.EditorUtilities;
using UnityEngine;

public class TextUi : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private TMP_Text _enemyText;

    [Header("Cart")]
    [SerializeField] private TMP_Text _catHealthText;
    [SerializeField] private TMP_Text _catAttackText;
    [SerializeField] private TMP_Text _catHungerText;

    [Header("Player")]
    [SerializeField] private TMP_Text _playerHealthText;
    [SerializeField] private TMP_Text _playerAttackText;
    [SerializeField] private TMP_Text _playerHungerText;

    private void OnEnable()
    {
        Cat.OnHeal += UpdateCatHealth;
        Cat.OnTakeDamage += UpdateCatHealth;
        Cat.OnPowerUp += UpdateCatAttack;
        Cat.OnHunger += UpdateCatHunger;

        Player.OnPlayerHeal += UpdatePlayerHealth;
        Player.OnPlayerTakeDamage += UpdatePlayerHealth;
        Player.OnPlayerAttack += UpdatePlayerAttack;
        Player.OnHunger += UpdatePlayerHunger;

        Enemy.OnEnemyTakeDamage += UpdateEnemyHealth;
    }

    private void OnDisable()
    {
        Cat.OnHeal -= UpdateCatHealth;
        Cat.OnTakeDamage -= UpdateCatHealth;
        Cat.OnPowerUp -= UpdateCatAttack;
        Cat.OnHunger -= UpdateCatHunger;
        Items.OnCatConsumeFood -= UpdateCatHunger;

        Player.OnPlayerHeal -= UpdatePlayerHealth;
        Player.OnPlayerTakeDamage -= UpdatePlayerHealth;
        Player.OnPlayerAttack -= UpdatePlayerAttack;
        Player.OnHunger -= UpdatePlayerHunger;
        Items.OnPlayerConsumeFood -= UpdatePlayerHunger;

        Enemy.OnEnemyTakeDamage -= UpdateEnemyHealth;
    }
    public void UpdateEnemyHealth(int Health)
    {
        _enemyText.text = "Enemy Health: " + Health;
    }

    public void UpdateCatHealth(int Health)
    {
        _catHealthText.text = "Cat Health: " + Health;
    }

    public void UpdateCatAttack(int Attack)
    {
        _catAttackText.text = "Cat Attack Damage: " + Attack;
    }

    public void UpdateCatHunger(float Hunger)
    {
        _catHungerText.text = "Cat Hunger: " + Hunger.ToString("F0");
    }

    public void UpdatePlayerHealth(int Health)
    {
        _playerHealthText.text = "Player Health: " + Health;
    }

    public void UpdatePlayerAttack(int Attack)
    {
        _playerAttackText.text = "Player Attack: " + Attack;
    }

    public void UpdatePlayerHunger(float Hunger)
    {
        _playerHungerText.text = "Player Hunger: " + Hunger.ToString("F0");
    }

}
