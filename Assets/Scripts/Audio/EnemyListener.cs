using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyListener : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Fuente de audio para reproducir el sonido

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que entra tiene el tag "enemy"
        if (collision.CompareTag("Enemy"))
        {
            PlayAlertSound();
        }
    }

    private void PlayAlertSound()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Reproduce el clip configurado en el AudioSource
        }
    }
}
