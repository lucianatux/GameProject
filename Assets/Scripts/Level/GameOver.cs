using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 


public class GameOver : MonoBehaviour
{
   // Referencia al componente TextMeshPro o Text de la UI
    public TextMeshProUGUI textUI;  // Cambia a Text si usas el sistema tradicional de Unity
    public string message = "Level 2 Completed! To be continued...";  // El mensaje que se va a mostrar
    public float displayTime = 2f;
    public float returnToMenuTime = 5f;  // Tiempo antes de regresar al menú principal


 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Activa el texto de la UI
            textUI.text = message;  // Asigna el mensaje al texto
            textUI.gameObject.SetActive(true);  // Activa el GameObject que contiene el texto

            // Oculta el texto después del tiempo especificado
            Invoke("HideText", displayTime);

            // Regresa al menú principal después del tiempo especificado
            Invoke("ReturnToMenu", returnToMenuTime);
        }
    }

     private void HideText()
    {
        textUI.gameObject.SetActive(false);  // Desactiva el texto después del tiempo
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");  // Cambia "Menu" al nombre exacto de tu escena de menú
    }
}
