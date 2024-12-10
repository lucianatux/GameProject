using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    [SerializeField] private Animator computerAnimator; // Animator del objeto Computer
    [SerializeField] private int totalAnimations = 3; // Número total de animaciones

    private bool isPlayerInZone = false; // Verifica si el jugador está en la zona
    private int currentAnimationIndex = 0; // Índice actual de la animación

    private static readonly int Index = Animator.StringToHash("index"); // Hash para optimizar el acceso al parámetro

    private void Update()
    {
        // Detectar interacción si el jugador está en la zona y presiona la tecla "F"
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.F))
        {
            if (computerAnimator != null)
            {
                // Cambiar al siguiente índice de animación
                currentAnimationIndex = (currentAnimationIndex + 1) % totalAnimations;
                computerAnimator.SetInteger(Index, currentAnimationIndex);

                Debug.Log($"Parámetro 'index' cambiado a: {currentAnimationIndex}");
            }
            else
            {
                Debug.LogWarning("El Animator no está asignado en el inspector.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tiene el tag "Player"
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
        }
    }
}
