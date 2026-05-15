using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] ScriptableObjectItem _scriptableObject;
    [SerializeField] LayerMask _catLayer;
    [SerializeField] LayerMask _playerLayer;
    [SerializeField] float _checkRadius = 2.0f;
    
    private bool _playerIsNear;

    public static Action<int> OnPlayerConsumeMedicine;
    public static Action<int> OnCatConsumeMedicine;
    public static Action<int, float> OnConsumePowerUp;

    private void Update()
    {
        if (_playerIsNear && Input.GetKeyDown(KeyCode.V))
        {

            Collider2D catCollider = Physics2D.OverlapCircle(transform.position, _checkRadius, _catLayer);

            if (catCollider != null)
            {
                CatConsumeMedicine();
                ConsumePowerUp();
            }
        }

        else if (_playerIsNear && Input.GetKey(KeyCode.G))
        {
            PlayerConsumeMedicine();
        }


    }

    public void PlayerConsumeMedicine()
    {
        OnPlayerConsumeMedicine?.Invoke(_scriptableObject.plusHealth);
        gameObject.SetActive(false);
    }

    public void CatConsumeMedicine()
    {
        OnCatConsumeMedicine?.Invoke(_scriptableObject.plusHealth);
        gameObject.SetActive(false);
    }

    public void ConsumePowerUp()
    {
        OnConsumePowerUp?.Invoke(_scriptableObject.plusPowerUp, _scriptableObject.duration);

        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = false;
        }
    }
}
