using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSide : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();
    }

   private void OnTriggerEnter2D(Collider2D collision)
    {
       // Verifica si el objeto que colisiona es el jugador
        if (collision.CompareTag("Player"))
        {
             // Reproduce el sonido
            if (audioSource != null)
            {
                audioSource.Play(); // Reproduce el audio
            }
        }
    }
}
