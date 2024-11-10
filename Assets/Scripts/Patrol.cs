using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
public float leftLimit = -4f;
    public float rightLimit = 4f;
    public float speed = 2f;
    public float fallSpeed = 5f; // Velocidad de caída al ser pisado
    public float fallDelay = 0.5f; // Retraso en segundos antes de comenzar a caer

    private bool movingRight = false;
    private bool isSteppedOn = false; // Controla si el mosquito fue pisado

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
        // Activa la caída continua del mosquito en el eje Y
        while (true)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            yield return null; // Continúa la caída en cada frame
        }
    }
}