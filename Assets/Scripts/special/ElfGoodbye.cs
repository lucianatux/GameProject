using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfGoodbye : MonoBehaviour
{
    [SerializeField] private GameObject elf; // Referencia al objeto "elf"
    private Animator elfAnimator; // Referencia al Animator de "elf"
    [SerializeField] private float destroyDelay = 0.01f; // Retraso antes de destruir al "elf"
    private bool hasAnimationTriggered = false; // Evitar múltiples activaciones


    private void Start()
    {
        // Asegúrate de que elf esté asignado y encuentra el Animator
        if (elf != null)
        {
            elfAnimator = elf.GetComponent<Animator>();
            if (elfAnimator == null)
            {
                Debug.LogError("No se encontró un Animator en el GameObject 'elf'.");
            }
        }
        else
        {
            Debug.LogError("El GameObject 'elf' no está asignado en el inspector.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         // Verifica si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player") && !hasAnimationTriggered)
        {
            hasAnimationTriggered = true; // Marca como activado
            Debug.Log("Player ha activado el trigger de Goodbye.");

            // Asegúrate de que el Animator exista antes de intentar modificarlo
            if (elfAnimator != null)
            {
                elfAnimator.SetBool("isGoing", true); // Activa la animación
                Debug.Log("El player activó la animación 'Goodbye' en el elf.");
                StartCoroutine(DestroyElfAfterAnimation());
            }
        }
    }

     private IEnumerator DestroyElfAfterAnimation()
    {
        // Espera a que termine la animación "goodbye"
        if (elfAnimator != null)
        {
            AnimatorStateInfo stateInfo = elfAnimator.GetCurrentAnimatorStateInfo(0);

            // Espera a que entre en la animación "goodbye"
            while (!stateInfo.IsName("goodbye"))
            {
                yield return null;
                stateInfo = elfAnimator.GetCurrentAnimatorStateInfo(0);
            }

            // Espera a que termine la animación
            yield return new WaitForSeconds(stateInfo.length + destroyDelay);

            // Desactiva el renderizado o colisión para ocultar el objeto
        SpriteRenderer spriteRenderer = elf.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }


             // Desactiva el parámetro para evitar reinicios de animación
            elfAnimator.SetBool("isGoing", false);

            // Destruye el GameObject "elf"
            Destroy(elf);
            Debug.Log("El GameObject 'elf' ha sido destruido.");
        }
    }
}
