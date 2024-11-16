using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    private Rigidbody2D rbGlobo;  // Referencia al Rigidbody2D del globo
    private Collider2D colGlobo;  // Referencia al Collider2D del globo

    // Llama este método al inicio
    private void Start()
    {
        // Obtén las referencias al Rigidbody2D y al Collider2D
        rbGlobo = GetComponent<Rigidbody2D>();
        colGlobo = GetComponent<Collider2D>();

        // Desactiva la gravedad del globo inicialmente
        rbGlobo.gravityScale = 0;  // Deja que el globo flote
        rbGlobo.isKinematic = true;  // Mantiene el globo sin afectar la física inicialmente
    }

    // Este método se llama cuando el jugador entra en el trigger del globo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Activa la física del globo para que caiga
            rbGlobo.isKinematic = false;  // Habilita la física para que el globo caiga
            rbGlobo.gravityScale = 1;  // Aplica gravedad para que el globo caiga hacia el suelo

            // Puedes hacer cualquier otra acción aquí si lo necesitas (animaciones, sonidos, etc.)
        }
    }
}
