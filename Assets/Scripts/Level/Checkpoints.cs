using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
   public void OnTriggerEnter2D(Collider2D other)
    {
      
        if (other.CompareTag("Player"))
        {
            // Llamamos al método del jugador para guardar su posición
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
            }
        }
    
    }
}
