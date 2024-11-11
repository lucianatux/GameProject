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
            // Destruir la moneda al ser recogida
            Destroy(gameObject);
        }
    }
}
