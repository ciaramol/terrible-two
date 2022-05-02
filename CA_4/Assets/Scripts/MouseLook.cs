using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{

    public Transform playerBody;
    public float mouseSensitivity = 100f;
    public float xRotation = 0f;
    Vector2 lookDirection;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        playerBody.Rotate(Vector3.up * lookDirection.x);
    }

    void OnLook(InputValue input)
    {
        lookDirection = mouseSensitivity * Time.deltaTime * input.Get<Vector2>();
    }

    private void Look()
    {
        xRotation -= lookDirection.y * mouseSensitivity * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    }
}
