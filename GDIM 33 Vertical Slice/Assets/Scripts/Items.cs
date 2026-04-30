using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    [SerializeField] private int _medicineStat = 10;
    [SerializeField] ScriptableObject _scriptableObject;

    //Press(v) to consume for cat
    //once consumed gives health to cat
    // currenthealth += Medicinestat

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
        OnConsumeMedicine?.Invoke(_medicineStat);
        gameObject.SetActive(false);
    }
}
