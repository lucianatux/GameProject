using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayOff : MonoBehaviour
{
    // Referencia al Animator
    private Animator animator;
    // Bandera para verificar si el jugador está en contacto
    private bool isPlayerColliding = false;
    // Tiempo para esperar antes de desactivar la animación
    public float sprayDuration = 1f;
    public bool isSpraying = false;

    // Start is called before the first frame update
    void Start()
    {
        // Obtener el Animator del GameObject actual
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si el jugador está en colisión y se presiona la tecla 'F'
        if (isPlayerColliding && Input.GetKeyDown(KeyCode.F))
        {
            // Activar el parámetro para iniciar la animación
            animator.SetBool("spraying", true);
            isSpraying = true;

            // Iniciar la corutina para desactivar la animación después del tiempo especificado
            StopCoroutine("StopSpraying");
            StartCoroutine("StopSpraying");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Detectar si el objeto colisiona con el jugador
        if (collider.CompareTag("Player"))
        {
            isPlayerColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        // Detectar si el jugador deja de colisionar con el objeto
        if (collider.CompareTag("Player"))
        {
            isPlayerColliding = false;
        }
    }

    // Corutina para detener la animación después de un tiempo
    private IEnumerator StopSpraying()
    {
        // Espera la duración especificada
        yield return new WaitForSeconds(sprayDuration);

        // Desactivar el parámetro de la animación
        animator.SetBool("spraying", false);
        isSpraying = false;
    }
}
