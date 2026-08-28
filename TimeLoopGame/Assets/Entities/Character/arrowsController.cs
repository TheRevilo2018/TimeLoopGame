using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class arrowsController : MonoBehaviour, IMoveTarget
{
    public float speed;
    public InputAction movement;
    public InputAction toggleRecord;

    private Vector2 currentForce = new Vector2(0, 0);
    private bool recording = false;
    private List<Vector3> inputs = new List<Vector3>() { Vector3.zero };
    private int inputIndex = 0;

    public Transform Target => transform;

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
        Vector3 dPos = new Vector3();
        if (recording)
        {
            dPos = new Vector3(currentForce.x * speed, 0, currentForce.y * speed);
            inputs.Add(dPos);
        }
        else
        {
            inputIndex++;
            if (inputIndex >= inputs.Count) inputIndex = 0;

            dPos = inputs[inputIndex];
        }

        transform.position += dPos;
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
