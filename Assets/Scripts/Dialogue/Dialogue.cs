using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Para usar imágenes
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines; // Líneas de diálogo
    [SerializeField] private Sprite[] dialogueImages; // Imágenes opcionales para cada línea
    [SerializeField] private GameObject dialoguePanel; // Panel de diálogo
    [SerializeField] private TMP_Text dialogueText; // Texto del diálogo
    [SerializeField] private Image dialogueImage; // Imagen que se mostrará (opcional)
    [SerializeField] private PlayerController playerController; // Referencia al script del jugador
    [SerializeField] private Animator playerAnimator; // Referencia al Animator del jugador

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    private float typingTime = 0.05f;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = dialogueLines[lineIndex];
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        ShowDialogueLine();
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            ShowDialogueLine();
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
        }
    }

    private void ShowDialogueLine()
    {
        dialogueText.text = string.Empty;

        playerController.SetDialogueState(true); // Indica que está en un diálogo
        playerAnimator.SetBool("isTalkingFront", true);

        StartCoroutine(TypeLine(dialogueLines[lineIndex]));

        // Configurar imagen para la línea actual
        if (lineIndex < dialogueImages.Length && dialogueImages[lineIndex] != null)
        {
            dialogueImage.sprite = dialogueImages[lineIndex];
            dialogueImage.gameObject.SetActive(true); // Mostrar imagen
        }
        else
        {
            dialogueImage.gameObject.SetActive(false); // Ocultar imagen si no hay
        }
    }

    private IEnumerator TypeLine(string line)
    {
        foreach (char ch in line)
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
        playerController.SetDialogueState(false); 
        playerAnimator.SetBool("isTalkingFront", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }

        if (didDialogueStart)
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
        }
    }
}
