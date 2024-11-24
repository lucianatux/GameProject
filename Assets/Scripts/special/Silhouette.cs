using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silhouette : MonoBehaviour
{
    private Color originalColor; // Guarda el color original del jugador
    private SpriteRenderer playerSpriteRenderer; // Referencia al SpriteRenderer del jugador

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra es el jugador
        if (other.CompareTag("Player"))
        {
            // Obtén el SpriteRenderer del jugador para cambiar el color
            playerSpriteRenderer = other.GetComponent<SpriteRenderer>();
            if (playerSpriteRenderer != null)
            {
                // Guarda el color original para restaurarlo después
                originalColor = playerSpriteRenderer.color;
                // Cambia el color a negro para la silueta
                playerSpriteRenderer.color = Color.black;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verifica si el objeto que sale es el jugador
        if (other.CompareTag("Player") && playerSpriteRenderer != null)
        {
            // Restaura el color original del jugador
            playerSpriteRenderer.color = originalColor;
        }
    }
}
