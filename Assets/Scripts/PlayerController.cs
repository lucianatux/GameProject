using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float groundDistanceVerifier;

    private Rigidbody2D rb;
    private int jumpCount = 0; // Contador de saltos
    public int maxJumps = 2; // Número máximo de saltos permitidos
    private bool isGrounded = false; // Indica si el personaje está en el suelo
    private bool facingRight = true; // Indica la dirección del personaje

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < maxJumps))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f); // Reinicia la velocidad en y antes del salto
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            isGrounded = false; // Desactiva el estado de suelo al saltar

            // Activar la animación de salto
            animator.SetBool("canJump", true); // Activa la animación al iniciar el salto
      
        } 
        // Empuje
            animator.SetBool("isPushing", Input.GetKey(KeyCode.E)); // Activa/desactiva mientras "E" esté presionada

            // Dar la vuelta
            animator.SetBool("isTurningAround", Input.GetKey(KeyCode.Z)); // Activa/desactiva mientras "Z" esté presionada

            // Hablar con el usuario
            animator.SetBool("isTalkingFront", Input.GetKey(KeyCode.X)); // Activa/desactiva mientras "X" esté presionada

            // Hablar de costado
            animator.SetBool("isTalkingSide", Input.GetKey(KeyCode.C)); // Activa/desactiva mientras "C" esté presionada

        

    }
    // Detecta colisión con el suelo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            var onGroundRay = Physics2D.Raycast(transform.position, Vector2.down, groundDistanceVerifier, LayerMask.NameToLayer("Player"));

            if (onGroundRay.collider != null)
            {

                isGrounded = true;
                jumpCount = 0; // Reinicia el contador de saltos al tocar el suelo

                // Permitir nuevamente la animación de salto
                animator.SetBool("canJump", false); // Desactiva la animación al tocar el suelo
            }
      
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
}


