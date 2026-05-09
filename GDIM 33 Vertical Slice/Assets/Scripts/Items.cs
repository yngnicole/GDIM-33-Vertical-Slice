using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    //[SerializeField] private int _medicineStat = 10;
    [SerializeField] ScriptableObjectItem _scriptableObject;
    [SerializeField] float _checkRadius = 2.0f;
    [SerializeField] LayerMask _catLayer;

    public static Action<int> OnConsumeMedicine;
    public static Action<int, float> OnConsumePowerUp;
    private bool _playerIsNear;

    private void Update()
    {
        if(_playerIsNear && Input.GetKeyDown(KeyCode.V))
        {
            
            Collider2D  catCollider = Physics2D.OverlapCircle(transform.position, _checkRadius, _catLayer);

            if (catCollider != null)
            {
                ConsumeMedicine();
                ConsumePowerUp();
            }
        }
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

    public void ConsumeMedicine()
    {
        OnConsumeMedicine?.Invoke(_scriptableObject.plusHealth);
        Debug.Log("invoked");
        
        gameObject.SetActive(false);
    }

    public void ConsumePowerUp()
    {
        OnConsumePowerUp?.Invoke(_scriptableObject.plusPowerUp, _scriptableObject.duration);

        gameObject.SetActive(false);
    }
}
