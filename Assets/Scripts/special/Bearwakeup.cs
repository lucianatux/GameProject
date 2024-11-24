using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bearwakeup : MonoBehaviour
{
     // Referencia al Animator
    private Animator animator;

    // Bandera para verificar si el jugador está en contacto
    private bool isPlayerColliding = false;

    private void Start()
    {
        // Obtener el Animator del GameObject actual
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Si el jugador está en colisión y se presiona la tecla 'E'
        if (isPlayerColliding && Input.GetKey(KeyCode.E))
        {
            // Activar el parámetro para iniciar la animación
            animator.SetBool("sheIsPushingMe", true);
        }
        else if ( !Input.GetKey(KeyCode.E))
        {
            // Desactivar el parámetro cuando la animación termina
            animator.SetBool("sheIsPushingMe", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar si el objeto colisiona con el jugador
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Detectar si el jugador deja de colisionar con el objeto
        if (collision.collider.CompareTag("Player"))
        {
            isPlayerColliding = false;
        }
    }
}
