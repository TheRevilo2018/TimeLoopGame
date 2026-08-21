using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public InputAction input;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var speed = input.ReadValue<Vector2>();

        rb.AddForce(new Vector3(speed.x, 0, speed.y));
    }
}
   