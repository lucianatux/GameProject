using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{

 public float leftLimit = -2f;
    public float rightLimit = 2f;
    public float speed = 2f;

    private bool movingRight = true;

void Start(){
    Flip();
}
    void Update()
    {
        // Movimiento en el eje X
        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                Flip(); // Llama al método Flip() para voltear el sprite
            }
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                Flip(); // Llama al método Flip() para voltear el sprite
            }
        }
    }

    // Método para voltear el sprite
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invierte la escala en el eje X
        transform.localScale = scale;
    }
}
