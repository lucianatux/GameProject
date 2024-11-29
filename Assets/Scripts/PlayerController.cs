using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 
using TMPro; 

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private IInteractable currentInteractable;
    public TextMeshProUGUI livesText; // Para usar TextMeshPro


    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int extraJump = 1;
    private bool facingRight = true; // Indica la dirección del personaje
    private int jumpCount = 0; // Contador de saltos
    private bool isGrounded = false; // Indica si el personaje está en el suelo
    private int currentLives;
    private bool isInDialogue = false; // Para saber si está en un diálogo



    // Inicializa los componentes necesarios del jugador (Rigidbody y Animator).
    // También establece la cantidad inicial de vidas del jugador.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentLives = 1;
    }

    // Update is called once per frame
    void Update()
    {
        HandleJump();     // Llamamos a la función de salto.
       

        if (canMove)
        {
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
            /* if (!isInDialogue) // Solo ejecuta esta lógica si no está en un diálogo
            {
                animator.SetBool("isTalkingFront", Input.GetKey(KeyCode.X));
            }*/

            // Hablar de costado
            /*animator.SetBool("isTalkingSide", Input.GetKey(KeyCode.C)); // Activa/desactiva mientras "C" esté presionada
*/
            //Activar elementos
            // Si el jugador presiona la tecla F, se ejecuta la interacción con el objeto cercano
            // si es que hay un objeto interactuable.
            animator.SetBool("isActivating", Input.GetKeyDown(KeyCode.F));
            
             if (Input.GetKeyDown(KeyCode.F))
            {
                // Ejecuta la interacción si hay un objeto cerca.
                if (currentInteractable != null)
                {
                    currentInteractable.Interact();
                }
            }
        
            // Si el jugador pierde todas las vidas, se reinicia la escena actual.
            if (currentLives <= 0)
            {
                RestartGame();
            }
        }
    }
    //Maneja el salto
    private void HandleJump()
    {
         // Verifica si el personaje está en el suelo usando OverlapCircle
        // Verifica si el jugador está tocando el suelo utilizando un círculo de colisión.
        // Si está en el suelo, reinicia el contador de saltos y desactiva la animación de salto.
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            jumpCount = 0; // Reinicia el contador de saltos al tocar el suelo
            animator.SetBool("isJumping", false); // Desactiva la animación de salto cuando está en el suelo
        }
        else
        {
            animator.SetBool("isJumping", true); // Mantiene la animación activa mientras está en el aire
        }
        // Salto
        // Detecta si el jugador presiona la tecla de salto (Space) y si puede saltar.
        // El salto es permitido si el jugador está en el suelo o si aún tiene saltos disponibles.
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < extraJump))
        {
                rb.velocity = new Vector2(rb.velocity.x, 0f); // Reinicia la velocidad en y antes del salto
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpCount++; // Incrementa el contador de saltos

                // Aseguramos que el contador no pase del máximo
                if (jumpCount > extraJump)
                {
                    jumpCount = extraJump; // Asegura que el salto no pase del maximo 
                }
        }
    }
    

    //Método para agregar un punto de vida al jugador
    public void AddLife()
        {
            currentLives++;
            UpdateLivesUI(); // Actualiza el texto en pantalla

        }
    //Método para restar un punto de vida al jugador
    public void LoseLife()
    {
        animator.SetBool("isDamaged", true);
        currentLives--;
        UpdateLivesUI(); // Actualiza el texto en pantalla
        animator.SetBool("isDamaged", false);


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

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = " " + currentLives; // Cambia el texto en pantalla
        }
        else
        {
            Debug.LogError("No se asignó el Text para mostrar las vidas.");
        }
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
    // Dibuja un gizmo en el editor para visualizar el área de comprobación del suelo
    // (usado para depuración).
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
    
    private bool canMove = true;
    // Método para desactivar el movimiento
    public void DisableMovement()
    {
        canMove = false;
    }
    // Método para reactivar el movimiento
    public void EnableMovement()
    {
        canMove = true;
    }

    // Al entrar en el área de colisión con un objeto interactuable,
    // se obtiene el componente IInteractable del objeto para interactuar con él.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentInteractable = collision.GetComponent<IInteractable>();
    }
    // Al salir del área de colisión con un objeto interactuable,
    // se elimina la referencia al objeto interactuable actual.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() == currentInteractable)
        {
            currentInteractable = null;
        }
    }

    // Método desde el sistema de diálogo para activar/desactivar el flag
    public void SetDialogueState(bool state)
    {
        isInDialogue = state;
    }


}


