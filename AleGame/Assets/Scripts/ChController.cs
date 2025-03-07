using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChController : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento
    public float jumpForce = 0.005f; // Fuerza de salto
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal"); // A y D
        float moveZ = Input.GetAxis("Vertical");   // W y S

        Vector3 movement = new Vector3(moveX, 0, moveZ).normalized * speed * Time.deltaTime;
        
        if (movement != Vector3.zero)
        {
            transform.forward = movement; // Hace que el modelo mire en la dirección del movimiento
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        transform.Translate(movement, Space.World);

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("IsJumping", true); // Primero activamos la animación
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Luego desactivamos isGrounded
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded)
            {
                isGrounded = true;
                animator.SetBool("IsJumping", false); // Al tocar suelo, desactivamos la animación
            }
        }
    }
}


