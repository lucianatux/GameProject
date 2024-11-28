using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public AudioSource audioSource; // Referencia al AudioSource
    public GameObject door; // Referencia a la puerta
    private SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer de la llave



    private void Start()
    {
        // Obtener el AudioSource, SpriteRenderer y Collider del mismo GameObject
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        // Desactiva el SpriteRenderer
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }
         // Destruye la puerta si hay una referencia válida
        if (door != null)
        {
            Destroy(door);
        }
        Destroy(gameObject, audioSource.clip.length); // Destruye la llave después de que termine el audio
    }
}
