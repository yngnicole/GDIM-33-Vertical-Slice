using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _playerTransform;
    private void Update()
    {
        //movement with WASD and arrow keys
        float movementX = Input.GetAxisRaw("Horizontal");
        float movementY = Input.GetAxisRaw("Vertical");

        UnityEngine.Vector2 movement = new UnityEngine.Vector2(movementX, movementY);

        transform.Translate(movement * _speed * Time.deltaTime);
    }
}