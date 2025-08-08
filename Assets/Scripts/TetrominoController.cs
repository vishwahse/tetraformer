using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TetrominoController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.7f;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject checker;
    [SerializeField] private GameObject next;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    void Update()
    {
        MoveCharacter();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnRotate()
    {
        transform.Rotate(0, 0, 90, Space.Self);
    }

    void MoveCharacter()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            Destroy(GetComponent<PlayerInput>());
            Destroy(GetComponent<TetrominoController>());
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            if (checker.activeSelf)
            {
                player.GetComponent<PlayerController>().enabled = true;
            }
            else
            {
                checker.SetActive(true);
                next.SetActive(false);
            }
        }

    }
}
