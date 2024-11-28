using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class DialogueBubble : MonoBehaviour
{
   [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private Sprite[] characterFaces; // Caras de los personajes
    [SerializeField] private bool[] isPlayerSpeaking; // Indica si el jugador habla en cada línea
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image playerImage; // Imagen del jugador (izquierda)
    [SerializeField] private Image npcImage; // Imagen del NPC (derecha)

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex;

    private float typingTime = 0.05f;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.X))
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
        UpdateDialogueUI(); // Actualizar UI inicial
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            UpdateDialogueUI(); // Actualizar UI en cada línea
            StartCoroutine(ShowLine());
        }
        else
        {
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
        }
    }

    private void UpdateDialogueUI()
    {
        // Cambiar la imagen según quién habla
        if (isPlayerSpeaking != null && lineIndex < isPlayerSpeaking.Length)
        {
            if (isPlayerSpeaking[lineIndex])
            {
                playerImage.sprite = characterFaces[lineIndex]; // Actualizar cara del jugador
                playerImage.gameObject.SetActive(true);
                npcImage.gameObject.SetActive(false); // Ocultar la del NPC
            }
            else
            {
                npcImage.sprite = characterFaces[lineIndex]; // Actualizar cara del NPC
                npcImage.gameObject.SetActive(true);
                playerImage.gameObject.SetActive(false); // Ocultar la del jugador
            }
        }
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;

        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("se puede iniciar dialogo");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("no se puede iniciar dialogo");

            if (didDialogueStart)
            {
                didDialogueStart = false;
                dialoguePanel.SetActive(false);
            }
        }
    }
}