using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookRespawn : MonoBehaviour
{
 public Vector3 respawnPosition; // Punto de respawn donde el libro reaparecerá
    public float respawnRotation = 90f; // Rotación horizontal en el eje Z en 2D
    private bool isFalling = false; // Detecta si el libro está cayendo
    private bool isPushed = false; // Detecta si el libro ha sido empujado
    private Rigidbody2D rb; // Referencia al Rigidbody2D del libro

    private void Start()
    {
        // Obtener el componente Rigidbody2D al inicio
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Iniciando libro, posición inicial: " + transform.position);
    }

    private void Update()
    {
        CheckIfPushed();
        CheckIfFalling();
    }

    private void CheckIfPushed()
    {
         // Detectar si el libro ha sido empujado en el eje X (movimiento del jugador)
        if (!isPushed && Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            isPushed = true;
            Debug.Log("El libro ha sido empujado.");
        }
    }

    private void CheckIfFalling()
    {
        // Detectar si el libro ha comenzado a caer (movimiento hacia abajo en el eje Y)
        if (isPushed && !isFalling && Mathf.Abs(rb.velocity.y) > 3f)
        {
            isFalling = true;
            Debug.Log("El libro ha comenzado a caer.");
            StartCoroutine(RespawnAfterDelay(1f)); // Espera 1 segundo antes de hacer respawn
        }
    }

    private System.Collections.IEnumerator RespawnAfterDelay(float delay)
    {
        // Mostrar el tiempo de espera
        Debug.Log("Esperando " + delay + " segundos antes de hacer respawn.");
        yield return new WaitForSeconds(delay);

        // Mueve el libro a la posición de respawn y ajusta su rotación
        transform.position = respawnPosition;
        transform.rotation = Quaternion.Euler(0, 0, respawnRotation);
        Debug.Log("El libro ha reaparecido en la posición: " + transform.position);

        // Desactivar la física para mantenerlo inmóvil
        rb.velocity = Vector2.zero;  // Detener cualquier velocidad que tenga
        rb.angularVelocity = 0;      // Detener cualquier movimiento angular
        rb.isKinematic = true;       // Desactivar la física
        rb.gravityScale = 0;         // Desactivar la gravedad
        Debug.Log("La física del libro ha sido desactivada.");

        // Reiniciar el estado para futuras interacciones
        isPushed = false;
        isFalling = false;
    }  
}
