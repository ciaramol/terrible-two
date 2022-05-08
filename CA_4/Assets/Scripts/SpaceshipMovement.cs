using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipMovement : MonoBehaviour
{
    public Transform shipTransform;
    public CharacterController controller;
    public float mouseSensitivityX = 100;
    public float mouseSensitivityY = 100;
    public float maxTurnSpeed = 100;
    public float speed = 12f;
    public float boostSpeedFactor = 2f;

    private Vector2 lookDirection;
    private Vector2 moveDirection;
    private bool boostOn = false;

    void Start()
    {
        // Adjust default values
        mouseSensitivityX /= 4; // 25 feels better, but 100 is a nicer number for them to pick from
        mouseSensitivityY /= 4;
        maxTurnSpeed = (maxTurnSpeed/100) * 0.75f;

        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Looking left and right
        float xMovement = lookDirection.x * Time.deltaTime * mouseSensitivityX;
        xMovement = Mathf.Clamp(xMovement, maxTurnSpeed * -1, maxTurnSpeed); // spaceship should not be able to rotate too fast
        shipTransform.Rotate(Vector3.up * xMovement, Space.World);

        // Looking up and down
        float yMovement = lookDirection.y * Time.deltaTime * mouseSensitivityY;
        yMovement = Mathf.Clamp(yMovement, maxTurnSpeed * -1, maxTurnSpeed);
        shipTransform.Rotate(Vector3.left * yMovement, Space.Self);

        // Translation
        float boostFactor = boostOn && moveDirection.y > 0? boostSpeedFactor : 1; // if going forward and boostOn, apply boostSpeedFactor
        Vector3 translation = transform.right * moveDirection.x + transform.forward * moveDirection.y * boostFactor;
        controller.Move(translation * Time.deltaTime * speed);
    }

    /// INPUT SYSTEM ///
    void OnLook(InputValue input)
    {
        lookDirection = input.Get<Vector2>();
        //Debug.Log($"OnLook triggered: (${lookDirection.x}, ${lookDirection.y})");
    }

    void OnMove(InputValue input)
    {
        moveDirection = input.Get<Vector2>();
        //Debug.Log($"OnLook triggered: (${moveDirection.x}, ${moveDirection.y})");
    }

    void OnBoost(InputValue input)
    {
        boostOn = input.Get<float>() == 1;
    }
}
