using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public AudioClip lifePointsSound; // El clip de sonido que se reproducirá al recoger magia
    private AudioSource audioSource; // Referencia al AudioSource del objeto

     private void Start()
    {
        // Obtener el componente AudioSource del objeto (suponiendo que ya lo has asignado en el Inspector)
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Verificamos si el objeto que colisiona es el jugador
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player != null)
            {
                player.AddLife(); // Añade una vida al jugador
                PlayLifePointsSound(); // Reproduce el sonido
            }

            Destroy(gameObject); // Destruye el objeto de magia
        }
    }
    // Método para reproducir el sonido de vida
    private void PlayLifePointsSound()
    {
        if (audioSource != null && lifePointsSound != null)
        {
            audioSource.PlayOneShot(lifePointsSound); // Reproduce el sonido una sola vez
        }
    }
}
