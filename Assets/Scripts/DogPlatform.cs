using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPlatform : MonoBehaviour
{   
    // Límites de movimiento en el eje X
    [SerializeField] private float leftLimit = 20f;
    [SerializeField] private float rightLimit = 10f;
    // Velocidad de patrullaje
    [SerializeField] private float speed = 2f;

    private bool movingRight = false;// Determina si el objeto está moviéndose hacia la derecha
    private Vector3 initialPosition; // Posición inicial para calcular los límites

   
   private void Start()
    {
        initialPosition = transform.position; 
        leftLimit = initialPosition.x - leftLimit;
        rightLimit = initialPosition.x + rightLimit;
    }

    private void Update()
    {
            PatrolDog();
    }

    private void PatrolDog()
    {
        // Movimiento de patrullaje en el eje X
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                Flip();
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                Flip();
            }
        }
    }

     private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
