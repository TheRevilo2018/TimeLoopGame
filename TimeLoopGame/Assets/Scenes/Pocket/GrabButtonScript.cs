using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabButtonScript : MonoBehaviour
{
    public Transform anchor;
    public float activateDistance = 0.2f;
    public string message;

    private bool activeLatch = false;
    private Rigidbody rb;
    private XRGrabInteractable grabbable;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabbable = GetComponent<XRGrabInteractable>();
    }


    private void FixedUpdate()
    {
        if (Vector3.Distance(anchor.position, transform.position) > activateDistance && !activeLatch)
        {
            if (!activeLatch)
            {
                activeLatch = true;
                grabbable.enabled = false;
                rb.linearVelocity = Vector3.zero;
                transform.position = anchor.position;
                transform.rotation = anchor.rotation;
                Debug.Log(message);
                grabbable.enabled = true;
            }
        }
        else
        {
            activeLatch = false;
        }
    }
}
