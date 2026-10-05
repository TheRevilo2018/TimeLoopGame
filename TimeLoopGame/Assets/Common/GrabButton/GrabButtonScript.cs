using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabButtonScript : MonoBehaviour
{
    public Transform grabTransform;
    public float activateDistance = 0.2f;
    public UnityEvent onPressed;

    private Rigidbody rb;
    private ParticleSystem particles;
    private IXRSelectInteractable grabbable;
    private XRInteractionManager interactionManager;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        particles = GetComponentInChildren<ParticleSystem>();
        grabbable = GetComponentInChildren<XRGrabInteractable>();
        interactionManager = FindAnyObjectByType<XRInteractionManager>();
    }


    private void FixedUpdate()
    {
        if (Vector3.Distance(grabTransform.position, transform.position) > activateDistance)
        {
            if (grabbable.isSelected)
            {
                interactionManager.CancelInteractableSelection(grabbable);
            }
            else
            {
                particles.Play();
                onPressed?.Invoke();
                grabTransform.SetPositionAndRotation(transform.position, transform.rotation);
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
