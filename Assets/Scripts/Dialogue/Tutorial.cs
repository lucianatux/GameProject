using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private string message; // Mensaje que se mostrará, configurable desde el Inspector
    [SerializeField] private GameObject dialoguePanel; // Panel de la burbuja de diálogo (UI)
    [SerializeField] private TMP_Text dialogueText; // Componente de texto dentro del panel
    [SerializeField] private float displayTime = 3f; // Tiempo que el panel estará visible (en segundos)

    private Coroutine hideDialogueCoroutine; // Referencia a la corrutina para desactivar el panel

    private void Start()
    {
        // Asegúrate de que el panel esté desactivado al inicio
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que entra es el jugador
        if (collision.CompareTag("Player"))
        {
            // Muestra el panel de diálogo
            if (dialoguePanel != null && dialogueText != null)
            {
                dialogueText.text = message; // Establece el mensaje personalizado
                dialoguePanel.SetActive(true); // Activa el panel

                // Si ya hay una corrutina activa para ocultar el panel, cancélala
                if (hideDialogueCoroutine != null)
                {
                    StopCoroutine(hideDialogueCoroutine);
                }

                // Inicia una nueva corrutina para ocultar el panel después del tiempo especificado
                hideDialogueCoroutine = StartCoroutine(HideDialogueAfterDelay());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Oculta el panel inmediatamente cuando el jugador sale del área
        if (collision.CompareTag("Player"))
        {
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);

                // Detén la corrutina si el jugador se aleja antes de que termine
                if (hideDialogueCoroutine != null)
                {
                    StopCoroutine(hideDialogueCoroutine);
                }
            }
        }
    }

    // Corrutina para ocultar el panel después de un tiempo
    private IEnumerator HideDialogueAfterDelay()
    {
        yield return new WaitForSeconds(displayTime); // Espera el tiempo especificado
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // Oculta el panel
        }
    }
}
