using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private float leftLimit;
    private float rightLimit;
    private float speed = 2f;
    private float fallSpeed = 10f; // Velocidad de caída al ser pisado
    private float fallDelay = 0.5f; // Retraso en segundos antes de comenzar a caer

    private bool movingRight = false;
    private bool isSteppedOn = false; // Controla si el mosquito fue pisado
    private Vector3 initialPosition; // Guardará la posición inicial del mosquito
    public int left;
    public int right;
    public AudioSource audioSource; // Referencia al AudioSource

    private SprayOff sprayOffScript; // Referencia al script SprayOff


    private void Start()
    {
         initialPosition = transform.position; // Guarda la posición inicial al iniciar el juego
        // Ajustamos los límites de patrullaje en base a la posición inicial
        leftLimit = initialPosition.x - left; // Establece el límite izquierdo con respecto a la posición inicial
        rightLimit = initialPosition.x + right; // Establece el límite derecho con respecto a la posición inicial
    
        // Obtener la referencia al script SprayOff en el objeto que tiene el controlador
        sprayOffScript = FindObjectOfType<SprayOff>();
        // Obtener el AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();
        // Verifica que el AudioSource se ha encontrado
        if (audioSource == null)
        {
            //Debug.LogError("¡No se encontró el componente AudioSource!");
        }
        else
        {
            //Debug.Log("AudioSource encontrado.");
        }
    }


    private void Update()
    {
        if (!isSteppedOn && (sprayOffScript == null || !sprayOffScript.isSpraying)) 
        {
            PatrolMosquito();
        }
         // Si isSpraying es true, los mosquitos caen
        if (sprayOffScript != null && sprayOffScript.isSpraying && !isSteppedOn) 
        {
              StartCoroutine(BeginFall());
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
                if (audioSource != null)
            {
                audioSource.Play(); // Reproduce el audio
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
        while (fallTime <10f) // Cae durante x segundos
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