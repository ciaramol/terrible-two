using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public CharacterController controller;
    Vector2 moveDirection;
    Vector2 lookDirection;
    Vector3 velocity;
    public float speed = 12f;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    public GameObject cutscene;

    public Transform playerBody;
    //public Transform cameraTransform;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        playerBody.Rotate(Vector3.up * lookDirection.x);
    }

    void OnMove(InputValue input)
    {
        moveDirection = Time.deltaTime * input.Get<Vector2>();
    }

    void Move()
    {
        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.y);
        //move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        controller.Move(move * speed * Time.deltaTime);
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
    }

    void OnInteract()
    {
        cutscene.SetActive(true);
    }
}
