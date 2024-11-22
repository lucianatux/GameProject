using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource

    private void Start()
    {
        // Obtener el AudioSource del mismo GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Este método se llama cuando otro collider entra en el trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Aquí es donde el jugador recoge la llave
            CollectKey();
        }
    }

    // Método para manejar la recogida de la llave
    private void CollectKey()
    {
          if (audioSource != null)
            {
                audioSource.Play(); // Reproduce el audio
            }
            Destroy(gameObject, audioSource.clip.length); // Destruye la llave después de que termine el audio
    }
}
