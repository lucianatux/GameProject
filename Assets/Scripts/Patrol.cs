using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private float leftLimit;
    private float rightLimit;
    private float speed = 2f;
    private float fallSpeed = 12f; // Velocidad de caída al ser pisado
    private float fallDelay = 0.3f; // Retraso en segundos antes de comenzar a caer

    private bool movingRight = false;
    private bool isSteppedOn = false; // Controla si el mosquito fue pisado
    private Vector3 initialPosition; // Guardará la posición inicial del mosquito


    private void Start()
    {
         initialPosition = transform.position; // Guarda la posición inicial al iniciar el juego
        // Ajustamos los límites de patrullaje en base a la posición inicial
        leftLimit = initialPosition.x - 3f; // Establece el límite izquierdo con respecto a la posición inicial
        rightLimit = initialPosition.x + 3f; // Establece el límite derecho con respecto a la posición inicial
    }


    private void Update()
    {
        if (!isSteppedOn)
        {
            PatrolMosquito();
        }
    }

    private void PatrolMosquito()
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta si el jugador está cayendo sobre el mosquito
        if (collision.gameObject.CompareTag("Player") && collision.relativeVelocity.y <= 0)
        {
            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 mosquitoPosition = transform.position;

            // Verifica si el contacto fue en la parte superior del mosquito
            if (contactPoint.y > mosquitoPosition.y && !isSteppedOn)
            {
                StartCoroutine(BeginFall()); // Inicia la corrutina para el retraso de la caída
            }
             
            else
            {
                // Si el mosquito pica al jugador por contacto en cualquier parte
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();

                if (player != null)
                {
                    player.LoseLife(); // Quita una vida al jugador
                }
            }
        }
    }

    private IEnumerator BeginFall()
    {
        isSteppedOn = true; // Marca al mosquito como pisado para detener el patrullaje
        yield return new WaitForSeconds(fallDelay); // Espera el tiempo definido en fallDelay
        StartCoroutine(Fall()); // Inicia la caída como una corrutina
    }

   
    private IEnumerator Fall()
    {
        isSteppedOn = true; // Detiene el patrullaje cuando es pisado
        float fallTime = 0f;

        while (fallTime < 25f) // Cae durante x segundos
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            fallTime += Time.deltaTime;
            yield return null;
        }

        // Reinicia el patrullaje en la posición inicial guardada en Start()
        isSteppedOn = false;
        transform.position = initialPosition; // Vuelve a la posición inicial
    }
}