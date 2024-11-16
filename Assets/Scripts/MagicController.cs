using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource

    private void Start()
    {
        // Obtener el AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();
        // Verifica que el AudioSource se ha encontrado
        if (audioSource == null)
        {
            Debug.LogError("¡No se encontró el componente AudioSource!");
        }
        else
        {
            Debug.Log("AudioSource encontrado.");
        }
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
            }
             // Reproducir el sonido
            if (audioSource != null)
            {
                Debug.Log("Reproduciendo audio...");
                audioSource.Play(); // Reproducir el audio
            }


            Destroy(gameObject); // Destruye el objeto de magia
        }
    }
   
}
