using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class GameOver : MonoBehaviour
{
   // Referencia al componente TextMeshPro o Text de la UI
    public TextMeshProUGUI textUI;  // Cambia a Text si usas el sistema tradicional de Unity
    public string message = "Level 2 Completed! To be continued...";  // El mensaje que se va a mostrar
    public float time = 2f;

    // Llama este método cuando otro collider entra en el trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Activa el texto de la UI
            textUI.text = message;  // Asigna el mensaje al texto
            textUI.gameObject.SetActive(true);  // Activa el GameObject que contiene el texto
        }
    }

    // que el texto desaparezca después de un tiempo
    public void HideTextAfterTime(float time)
    {
        Invoke("HideText", time);
    }

    private void HideText()
    {
        textUI.gameObject.SetActive(false);  // Desactiva el texto después del tiempo
    }
}
