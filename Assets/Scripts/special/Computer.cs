using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{
   [SerializeField]
    private Animator animator; // Referencia al Animator

    [SerializeField]
    private int totalAnimations = 3; // Número total de animaciones 

    private int currentAnimationIndex = 0; // Índice actual de la animación
    private static readonly int AnimationIndex = Animator.StringToHash("index");

    private void Start()
    {
        if (animator == null)
        {
            Debug.LogError("No se encontró un Animator. Por favor, asígnalo manualmente.");
        }
    }

    public void Interact()
    {
        if (animator != null)
        {
            // Cambiar al siguiente índice de animación
            currentAnimationIndex = (currentAnimationIndex + 1) % totalAnimations;
            animator.SetInteger(AnimationIndex, currentAnimationIndex);

            Debug.Log($"Cambiando a animación {currentAnimationIndex}");
        }
        else
        {
            Debug.LogWarning("No se encontró un Animator para controlar las animaciones.");
        }
    }
}
