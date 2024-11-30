using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Asegúrate de incluir esta línea para usar TextMeshPro

public class Bearwakeup : MonoBehaviour
{
    // Referencia al Animator
    private Animator animator;

    // Bandera para verificar si el jugador está en contacto
    private bool isPlayerColliding = false;

    // Referencia al panel de la UI
    [SerializeField] private GameObject uiPanel;

    // Referencia al texto dentro del panel
    [SerializeField] private TextMeshProUGUI uiText;

    // Tiempo que el panel estará activo
    [SerializeField] private float panelDisplayTime = 2f;

    // Texto que se mostrará en el panel (editable desde el inspector)
    [SerializeField] private string panelMessage;

    // Bandera para controlar si el panel está activo
    private bool isPanelActive = false;

    private void Start()
    {
        // Obtener el Animator del GameObject actual
        animator = GetComponent<Animator>();

        // Asegurarse de que el panel esté desactivado al inicio
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    private void Update()
    {
        // Si el jugador está en colisión y se presiona la tecla 'E'
        if (isPlayerColliding && Input.GetKey(KeyCode.E))
        {
            // Activar el parámetro para iniciar la animación
            animator.SetBool("sheIsPushingMe", true);

            // Mostrar el panel de UI si no está activo
            if (!isPanelActive)
            {
                StartCoroutine(ShowUIPanel());
            }
        }
        else if (!Input.GetKey(KeyCode.E))
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

    // Corutina para mostrar el panel de UI
    private IEnumerator ShowUIPanel()
    {
        if (uiPanel != null && uiText != null)
        {
            // Configurar el texto del panel
            uiText.text = panelMessage;

            // Activar el panel
            uiPanel.SetActive(true);
            isPanelActive = true;

            // Esperar el tiempo especificado
            yield return new WaitForSeconds(panelDisplayTime);

            // Desactivar el panel
            uiPanel.SetActive(false);
            isPanelActive = false;
        }
    }
}
