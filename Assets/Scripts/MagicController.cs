using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
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

            Destroy(gameObject); // Destruye el objeto de magia
        }
    }
}
