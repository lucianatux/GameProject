using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPlatform : MonoBehaviour
{
    private float leftLimit;
    private float rightLimit;
    private float speed = 2f;
    private bool movingRight = false;
    private Vector3 initialPosition;

   
   private void Start()
    {
        initialPosition = transform.position; 
        leftLimit = initialPosition.x - 10f; 
        rightLimit = initialPosition.x + 10f; 
    }

    private void Update()
    {
            PatrolDummy();
    }

    private void PatrolDummy()
    {
        // Movimiento de patrullaje en el eje X
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Asegurarte de que SetParent se llama solo si el objeto está activo.
            if (gameObject.activeInHierarchy)
            {
                collision.transform.SetParent(null);
            }
            else
            {
                Debug.LogWarning("El objeto está desactivado, no se pudo desasociar al Player.");
            }
        }
    }
}

