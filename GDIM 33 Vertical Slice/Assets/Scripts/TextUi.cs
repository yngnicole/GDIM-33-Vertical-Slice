using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;

public class TextUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _enemyText;
    [SerializeField] private TMP_Text _catText;

    public void UpdateEnemyHealth(int Health)
    {
        _enemyText = "Enemy Health: " + Health;
    }

    public void UpdateCatHealth(int Health)
    {
        _catText = "Cat Health: " + Health;
    }
}
