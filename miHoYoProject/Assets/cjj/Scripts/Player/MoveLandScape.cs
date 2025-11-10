using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveLandScape : MonoBehaviour
{
    public InputAction rotateAction;
    public InputAction moveAction;

    public InputAction verticalAction;

    public PlayerGameplay playerGameplay;

    public float rotateSpeed = 0f;

    public float moveSpeed = 70f;

    void Start()
    {
        
    }

    void OnEnable()
    {
        rotateAction.Enable();
        moveAction.Enable();
        verticalAction.Enable();
        // rotateAction.performed += RotateLandscape;
    }

    void OnDisable()
    {
        rotateAction.Disable();
        moveAction.Disable();
        verticalAction.Disable();
        // rotateAction.performed -= RotateLandscape;
    }
    void FixedUpdate()
    {
        if (playerGameplay.chosenLandscape != null)
        {
            float rotateInput = rotateAction.ReadValue<float>();
            if (rotateInput != 0f)
            {
                Debug.Log("Rotate Input: " + rotateInput);
                playerGameplay.chosenLandscape.GetComponent<Rigidbody>().MoveRotation(
                    playerGameplay.chosenLandscape.transform.rotation * Quaternion.Euler(0, rotateInput * rotateSpeed * Time.fixedDeltaTime, 0)
                );
            }

            Vector2 moveInput = moveAction.ReadValue<Vector2>().normalized;

            if (moveInput != Vector2.zero)
            {
                Debug.Log("Move Input: " + moveInput);
                playerGameplay.chosenLandscape.GetComponent<Rigidbody>().MovePosition(
                    playerGameplay.chosenLandscape.transform.position +
                    new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed * Time.fixedDeltaTime
                );
            }
            
            if(verticalAction.ReadValue<float>() != 0f)
            {
                float verticalInput = verticalAction.ReadValue<float>();
                Debug.Log("Vertical Input: " + verticalInput);
                playerGameplay.chosenLandscape.GetComponent<Rigidbody>().MovePosition(
                    playerGameplay.chosenLandscape.transform.position +
                    new Vector3(0, verticalInput, 0) * moveSpeed * Time.fixedDeltaTime
                );
            }
        }
    }

    void RotateLandscape(InputAction.CallbackContext context)
    {
        float rotateInput = context.ReadValue<float>();
        Debug.Log("Rotate Input: " + rotateInput);
        if (playerGameplay.chosenLandscape != null)
        {
            playerGameplay.chosenLandscape.transform.Rotate(Vector3.up, rotateInput * rotateSpeed * Time.deltaTime);
        }
    }
    
}
