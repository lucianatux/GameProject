using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogPlatform : MonoBehaviour
{
    private float leftLimit;
    private float rightLimit;
    private float speed = 3f;
    private bool movingRight = false;
    private Vector3 initialPosition;

   
   private void Start()
    {
        initialPosition = transform.position; 
        leftLimit = initialPosition.x - 20f; 
        rightLimit = initialPosition.x + 10f; 
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