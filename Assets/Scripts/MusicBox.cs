using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public Animator playerAnimator; // Referencia al Animator del jugador
    public AudioSource audioSource; // Referencia al AudioSource para la música
    public float turnAroundDuration = 14f; // Duración en segundos del "turnaround"

    private bool isInsideTrigger = false; // Determina si el jugador está dentro del área
    private float timer = 0f; // Temporizador para controlar la duración del "turnaround"

    private void Update()
    {
        // Si el jugador está dentro del área de la caja musical y el temporizador está corriendo
        if (isInsideTrigger)
        {
            // Temporizador que simula la tecla Z presionada por x segundos
            if (timer < turnAroundDuration)
            {
                timer += Time.deltaTime; // Aumenta el tiempo transcurrido
                playerAnimator.SetBool("isTurningAround", true); // Mantiene la animación activada
            }
            else
            {
                // Después de x segundos, desactiva la animación
                playerAnimator.SetBool("isTurningAround", false);
                isInsideTrigger = false; // Desactiva el área de influencia
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproduce la música si no está reproduciéndose ya
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // Inicia el temporizador al entrar en el área
            isInsideTrigger = true;
            timer = 0f; // Reinicia el temporizador
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Detiene la música cuando el jugador sale del área
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            // Detiene la animación inmediatamente si el jugador sale del área
            playerAnimator.SetBool("isTurningAround", false);
            isInsideTrigger = false; // Detiene la acción de "turnaround"
            timer = 0f; // Reinicia el temporizador
        }
    }
}
