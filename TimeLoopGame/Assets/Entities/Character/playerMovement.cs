using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float maxForce = 10f;
    public float maxDistance = 2;

    public MonoBehaviour targetObject;
    private IMoveTarget target;

    private void Start()
    {
        target = (IMoveTarget)targetObject;
    }

    private void FixedUpdate()
    {
        var force = (target.Target.position - transform.position) * (1 / Time.fixedDeltaTime);
        rb.linearVelocity = clampVector3(force);
    }

    private Vector3 clampVector3(Vector3 v)
    {
        var mag = v.magnitude;
        if (mag < maxForce) return v;

        return v * (maxForce / mag);
    }
}
