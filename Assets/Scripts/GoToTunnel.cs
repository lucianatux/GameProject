using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTunnel : MonoBehaviour
{
   [Tooltip("El destino al que se teletransportará el jugador")]
    public Transform TunnelStart;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra en el Trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Cambia la posición del jugador al destino
            other.transform.position = TunnelStart.position;
        }
    }
}
