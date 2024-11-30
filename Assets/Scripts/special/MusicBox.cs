using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator; // Referencia al Animator del jugador
    [SerializeField] private PlayerController playerController; // Referencia al script del jugador
    [SerializeField] private AudioSource audioSource; // Referencia al AudioSource para la música

    private bool isPlayerInRange= false;

    private void Update()
    {
        // Si el jugador está dentro del área de la caja musical 
        if (isPlayerInRange)
        {
            playerController.SetMusicBoxState(true); // Indica que está en la caja musical
            playerAnimator.SetBool("isTurningAround", true); // Mantiene la animación activada
        }
        else
        {
            //  desactiva la animación
            playerController.SetMusicBoxState(false); 
            playerAnimator.SetBool("isTurningAround", false);
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
            isPlayerInRange = true;
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
            isPlayerInRange = false;

        }
    }
}
