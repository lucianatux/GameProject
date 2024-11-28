using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] private GameObject socketOff; // GameObject que se activará o desactivará
    [SerializeField] private Animator fanAnimator; // Animator del objeto fan
    [SerializeField] private GameObject wind; // Gameobject viento que bloquea el camino o desaparece


    private bool isPlayerInZone = false; // Verifica si el jugador está en la zona


    private void Update()
    {
        // Detectar interacción si el jugador está en la zona y presiona la tecla "F"
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.F))
        {
           if (socketOff != null && fanAnimator != null && wind != null)
            {
                // Alternar el estado activo/inactivo del objeto socketOff
                bool isActive = !socketOff.activeSelf;
                bool windIsBlowing = !wind.activeSelf;
                socketOff.SetActive(isActive);
                wind.SetActive(windIsBlowing);

                // Actualizar el parámetro "isOff" en el Animator
                fanAnimator.SetBool("isOff", isActive);

                Debug.Log($"socketOff está {(isActive ? "activado" : "desactivado")}, fan animación {(isActive ? "FanOff" : "Idle")}");
            }
            else
            {
                Debug.LogWarning("socketOff o fanAnimator no están asignados.");
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

