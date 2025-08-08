using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    [SerializeField] private float jumpForce;
    private Animator anim;
    private SpriteRenderer sR;
    public bool isGrounded = true;
    private Vector3 moveInput3D;
    [SerializeField] string sceneName;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sR = GetComponent<SpriteRenderer>();
    }


    void FixedUpdate()
    {
        MoveCharacter();
        if (moveInput != new Vector2(0, 0))
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        Flip();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (isGrounded == true)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void Flip()
    {
        if (moveInput == new Vector2(1, 0))
        {
            sR.flipX = false;
        }
        else if (moveInput == new Vector2(-1, 0))
        {
            sR.flipX = true;
        }
    }
    void MoveCharacter()
    {
        //rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        moveInput3D = moveInput;
        transform.position += moveInput3D * moveSpeed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Flag"))
        {
            print("you found the flag!");
            SceneManager.LoadScene(sceneName);
        }
    }
}
