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
    public static Action<float> OnCatConsumeFood;
    public static Action<float> OnPlayerConsumeFood;
    private void Update()
    {
        if (!_playerIsNear) return;
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            Collider2D catCollider = Physics2D.OverlapCircle(transform.position, _checkRadius, _catLayer);

            if (catCollider != null)
            {
                //CatConsumeMedicine();
                //ConsumePowerUp();
                //CatConsumeFood();
                OnCatConsumeMedicine?.Invoke(_scriptableObject.plusHealth);
                OnConsumePowerUp?.Invoke(_scriptableObject.plusPowerUp, _scriptableObject.duration);
                OnCatConsumeFood?.Invoke(_scriptableObject.plusHunger);

                gameObject.SetActive(false);
            }
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            // PlayerConsumeMedicine();
            //PlayerConsumeFood();

            if (_scriptableObject.plusPowerUp > 0)
            {
                return;
            }

            OnPlayerConsumeMedicine?.Invoke(_scriptableObject.plusHealth);
            OnPlayerConsumeFood?.Invoke(_scriptableObject.plusHunger);

            gameObject.SetActive(false);
        }


    }

    /*public void PlayerConsumeMedicine()
    {
        OnPlayerConsumeMedicine?.Invoke(_scriptableObject.plusHealth);
        gameObject.SetActive(false);
    }

    public void PlayerConsumeFood()
    {
        OnPlayerConsumeFood?.Invoke(_scriptableObject.plusHunger);
        gameObject.SetActive(false);
    }
    public void CatConsumeMedicine()
    {
        OnCatConsumeMedicine?.Invoke(_scriptableObject.plusHealth);
        gameObject.SetActive(false);
    }

    public void CatConsumeFood()
    {
        OnCatConsumeFood?.Invoke(_scriptableObject.plusHunger);
        gameObject.SetActive(false);
    }



    public void ConsumePowerUp()
    {
        OnConsumePowerUp?.Invoke(_scriptableObject.plusPowerUp, _scriptableObject.duration);

        gameObject.SetActive(false);
    }

    */


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
