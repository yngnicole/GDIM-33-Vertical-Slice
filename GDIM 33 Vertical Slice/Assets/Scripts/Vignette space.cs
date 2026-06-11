using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignettespace : MonoBehaviour
{
    [SerializeField] private Material vignetteMaterial;
    [SerializeField] private string shaderPropertyName = "_Vignette_Intensity";
    [SerializeField] private float _fadeSpeed = 4f;
    [SerializeField] private float _maxIntensity = 1f;

    private float _targetIntensity = 0f;
    private float _currentIntensity = 0f;
    void Start()
    {
        vignetteMaterial.SetFloat(shaderPropertyName, 0f);
    }


    void Update()
    {
        _currentIntensity = Mathf.MoveTowards(_currentIntensity, _targetIntensity, _fadeSpeed * Time.deltaTime);
        vignetteMaterial.SetFloat(shaderPropertyName, _currentIntensity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            _targetIntensity = _maxIntensity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _targetIntensity = 0f;
        }
    }

}
