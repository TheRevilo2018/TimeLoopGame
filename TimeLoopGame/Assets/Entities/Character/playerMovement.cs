using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    const float FORCE_MULT = 10;

    public Rigidbody rb;
    public InputAction movement;
    public InputAction toggleRecord;

    private Vector2 currentForce = new Vector2(0, 0);
    private bool recording = false;
    private List<Vector3> inputs = new List<Vector3>();
    private int inputIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggleRecord.performed += ToggleRecord_performed;
    }

    private void OnEnable()
    {
        movement.Enable();
        toggleRecord.Enable();
    }

    private void OnDisable()
    {
        movement.Disable();
        toggleRecord.Disable();
        recording = false;
    }

    private void FixedUpdate()
    {
        Vector3 newForce = new Vector3();
        if (recording)
        {
            newForce = new Vector3(currentForce.x * FORCE_MULT, 0, currentForce.y * FORCE_MULT);
            inputs.Add(newForce);
        }
        else
        {
            inputIndex++;
            if (inputIndex >= inputs.Count) inputIndex = 0;

            newForce = inputs[inputIndex];
        }

        rb.AddForce(newForce);
    }

    private void Update()
    {
        currentForce = movement.ReadValue<Vector2>();
    }

    private void ToggleRecord_performed(InputAction.CallbackContext obj)
    {
        toggleRecording();
    }

    private void toggleRecording()
    {
        if (!recording)
        {
            inputs.Clear();
            inputIndex = 0;
        }
        recording = !recording;
    }
}
