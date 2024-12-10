using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private Transform player;  // Referencia al jugador
    [SerializeField] private float floatHeight = 3f;  // Altura a la que el globo debe estar sobre el jugador
    [SerializeField] private float ascentSpeed = 2f;  // Velocidad de ascenso del globo
    [SerializeField] private float ceilingHeight = 34f;  // Altura máxima hasta donde el globo puede subir (techo)
    [SerializeField] private GameObject balloonUp;  // Referencia al objeto BalloonUp que hace que el globo suba

    private bool isAttached = false;  // Si el globo está adherido al jugador
    private bool isAscending = false;  // Si el globo está ascendiendo

    void Update()
    {
        // Si el globo está adherido al jugador, mantenerlo en su posición
        if (isAttached && !isAscending)
        {
            transform.position = player.position + Vector3.up * floatHeight;
        }
    }

    // Este método se llama cuando el globo entra en contacto con el jugador
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttached = true;  // El globo se adhiere al jugador
        }

        // Si el globo entra en contacto con el objeto BalloonUp, hacer que suba
        if (other.CompareTag("BalloonUp"))
        {
            isAttached = false;  // El globo deja de estar adherido al jugador
            StartCoroutine(MoveBalloonUp());  // Comienza el ascenso del globo
        }
    }

    // Este método se llama cuando el jugador deja de estar en contacto con el globo
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttached = false;  // El globo deja de estar adherido al jugador
        }
    }

    // Coroutine para mover el globo hacia arriba hasta el techo
    private IEnumerator MoveBalloonUp()
    {
        isAscending = true;  // El globo comienza a ascender

    while (transform.position.y < ceilingHeight)
    {
        // Mover el globo hacia arriba lentamente sin usar físicas, solo transform
        transform.position = new Vector3(transform.position.x, transform.position.y + ascentSpeed * Time.deltaTime, transform.position.z);
        yield return null;  // Esperar un frame
    }

    // Asegurar que el globo quede exactamente en el techo
    transform.position = new Vector3(transform.position.x, ceilingHeight, transform.position.z);
    isAscending = false;  // El globo deja de ascender
    }

}