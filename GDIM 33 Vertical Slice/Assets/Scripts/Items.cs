using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private int _medicineStat = 10;
    [SerializeField] ScriptableObjectItem _scriptableObject;

    public static Action<int> OnConsumeMedicine;


    private void OnEnable()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            ConsumeMedicine();
        }

    }

    public void ConsumeMedicine()
    {
        OnConsumeMedicine?.Invoke(_scriptableObject.plusHealth);
        Debug.Log("invoked");
        
        gameObject.SetActive(false);
    }
}
