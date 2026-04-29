using System.Collections;
using System.Collections.Generic;
using TMPro;
//using TMPro.EditorUtilities;
using UnityEngine;

public class TextUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyText;
    [SerializeField] private TMP_Text _catText;

    private void OnEnable()
    {
        Cat.OnHeal += UpdateCatHealth;
        Cat.OnTakeDamage += UpdateCatHealth;
        Enemy.OnEnemyTakeDamage += UpdateEnemyHealth;
    }

    private void OnDisable()
    {
        Cat.OnHeal -= UpdateCatHealth;
        Cat.OnTakeDamage -= UpdateCatHealth;
        Enemy.OnEnemyTakeDamage -= UpdateEnemyHealth;
    }
    public void UpdateEnemyHealth(int Health)
    {
        _enemyText.text = "Enemy Health: " + Health;
    }

    public void UpdateCatHealth(int Health)
    {
        _catText.text = "Cat Health: " + Health;
    }
}
