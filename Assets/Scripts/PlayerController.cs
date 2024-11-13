using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
     
    private float speed = 5f;
    private bool facingRight = true; // Indica la dirección del personaje

    private float jumpForce = 10f;
    public Transform groundCheck; // Transform del punto de verificación del suelo
    public float groundCheckRadius = 0.2f; // Radio de detección del suelo
    public LayerMask groundLayer; // Layer del suelo para verificar colisiones
    private int jumpCount = 0; // Contador de saltos
    private int maxJumps = 1; // Número máximo de saltos permitidos ademas del salto normal
    private bool isGrounded = false; // Indica si el personaje está en el suelo
    private int currentLives;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentLives = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Verifica si el personaje está en el suelo usando OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            jumpCount = 0; // Reinicia el contador de saltos al tocar el suelo
            animator.SetBool("isJumping", false); // Desactiva la animación de salto cuando está en el suelo
        }

        // Movimiento horizontal
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        animator.SetFloat("Horizontal", Mathf.Abs(moveInput));

        // Giro del personaje
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }

        // Empuje
        animator.SetBool("isPushing", Input.GetKey(KeyCode.E)); // Activa/desactiva mientras "E" esté presionada

        // Dar la vuelta
        animator.SetBool("isTurningAround", Input.GetKey(KeyCode.Z)); // Activa/desactiva mientras "Z" esté presionada

        // Hablar con el usuario
        animator.SetBool("isTalkingFront", Input.GetKey(KeyCode.X)); // Activa/desactiva mientras "X" esté presionada

        // Hablar de costado
        animator.SetBool("isTalkingSide", Input.GetKey(KeyCode.C)); // Activa/desactiva mientras "C" esté presionada

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < maxJumps))
        {
            if (isGrounded) 
            {
                // Si está en el suelo, hace un salto y activa la animación
                animator.SetBool("isJumping", true); // Activa la animación de salto al iniciar el salto
            }
            else
            {
                // Si no está en el suelo, continúa permitiendo el salto adicional
                animator.SetBool("isJumping", true); // Mantiene la animación de salto mientras en el aire
            }

            rb.velocity = new Vector2(rb.velocity.x, 0f); // Reinicia la velocidad en y antes del salto
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++; // Incrementa el contador de saltos

            // Aseguramos que el contador no pase del máximo
            if (jumpCount > maxJumps)
            {
                jumpCount = maxJumps; // Asegura que el salto no pase de 2
            }
        }

        // Si el personaje toca el suelo, reinicia el contador de saltos
        if (isGrounded)
        {
            jumpCount = 0; // Reinicia el contador de saltos
        }

        // Detener la animación de salto cuando el personaje está en el aire o en el suelo
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true); // Mantiene la animación activa mientras está en el aire
        }
        // Reinicio del juego si las vidas se reducen a cero

         if (currentLives <= 0)
        {
            RestartGame();
        }
    }

    //Método para agregar vidas
    public void AddLife()
        {
            currentLives++;
            Debug.Log("Vida añadida. Vidas actuales: " + currentLives);
        }
    //Método para perder vidas
    public void LoseLife()
    {
        currentLives--;
        Debug.Log("Vida perdida. Vidas restantes: " + currentLives);

        if (currentLives <= 0)
        {
            RestartGame();
        }
    }
    //Reinicia el juego
    private void RestartGame()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // Método para voltear la dirección del personaje
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invierte la escala en el eje X
        transform.localScale = scale;
    }

    // Visualización del groundCheck en el editor
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}


