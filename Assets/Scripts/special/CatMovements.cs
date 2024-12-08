using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovements : MonoBehaviour
{
    [SerializeField] private Animator animator; // Referencia al Animator del gato
    [SerializeField] private GameObject blockArea; // Referencia al hijo blockArea
    [SerializeField] private GameObject book; // Referencia al objeto Book
    [SerializeField] private float fallThreshold = 3f; // Distancia mínima para considerar una caída
    [SerializeField] private AudioSource sleepAudioSource; // Fuente de audio para el sonido de dormir
    [SerializeField] private AudioSource alertAudioSource; // Fuente de audio para el sonido de alerta


 
    private bool isSleeping = false; // Estado del gato

    private Vector3 previousBookPosition; // Posición anterior del libro

     private void Start()
    {
        // Inicializa la posición anterior del libro
        if (book != null)
        {
            previousBookPosition = book.transform.position;
        }
    }

    private void Update()
    {
        // Verifica si el gato ya está durmiendo
        if (isSleeping) return;

        // Verifica si el libro ha caído
        if (book != null && HasBookFallen())
        {
            SetSleepingState(true);
        }
    }
 
    private bool HasBookFallen()
    {
            // Obtén el componente Rigidbody2D del libro
        Rigidbody2D rb = book.GetComponent<Rigidbody2D>();

        // Asegúrate de que el libro tiene un Rigidbody2D
        if (rb == null)
        {
            Debug.LogWarning("El objeto 'book' no tiene un Rigidbody2D.");
            return false;
        }

        // Verifica si la velocidad vertical del libro es mayor que el umbral
        if (Mathf.Abs(rb.velocity.y) > fallThreshold) // Cambia "3f" según el umbral que consideres como caída
        {
            return true;
        }

        return false;
    }

    private void SetSleepingState(bool sleeping)
    {

        isSleeping = sleeping;

        // Configura el parámetro del Animator
        if (animator != null)
        {
            animator.SetBool("isSleeping", sleeping);
        }

        // Si el gato se duerme, desactiva blockArea
        if (blockArea != null)
        {
            blockArea.SetActive(!sleeping);
        }
         if (sleepAudioSource != null)
        {
            sleepAudioSource.Play(); // Reproduce el clip configurado en el AudioSource
        }
        else
        {
            Debug.LogWarning("Falta el sleepAudioSource");
        }

        Debug.Log("El gato ahora está durmiendo.");
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSleeping) return; // Ignorar lógica de alerta si el gato está durmiendo

        // Verifica si el objeto que entra en catArea tiene el tag del jugador
        if (collision.CompareTag("Player"))
        {
            SetAlertState(true); // Activar estado de alerta
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isSleeping) return; // Ignorar lógica de alerta si el gato está durmiendo

        // Verifica si el objeto que sale de catArea tiene el tag del jugador
        if (collision.CompareTag("Player"))
        {
            SetAlertState(false); // Desactivar estado de alerta
        }
    }

    private void SetAlertState(bool isAlert)
    {
        // Configura el parámetro del Animator
        if (animator != null)
        {
            animator.SetBool("isAlert", isAlert);
        }

        // Activa o desactiva el GameObject blockArea
        if (blockArea != null)
        {
            blockArea.SetActive(isAlert);
        }

         // Reproduce o detiene el sonido de alerta
        if (alertAudioSource != null)
        {
            if (isAlert && !alertAudioSource.isPlaying)
            {
                alertAudioSource.Play();
            }
            else if (!isAlert && alertAudioSource.isPlaying)
            {
                alertAudioSource.Stop();
            }
        }
    }
}
