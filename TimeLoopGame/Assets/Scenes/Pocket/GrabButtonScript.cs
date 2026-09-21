using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabButtonScript : MonoBehaviour
{
    public Transform anchor;
    public float activateDistance = 0.2f;
    public string message;

    private Rigidbody rb;
    private ParticleSystem particles;
    private IXRSelectInteractable grabbable;
    private XRInteractionManager interactionManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        particles = GetComponentInChildren<ParticleSystem>();
        grabbable = GetComponent<XRGrabInteractable>();
        interactionManager = FindAnyObjectByType<XRInteractionManager>();
    }


    private void FixedUpdate()
    {
        if (Vector3.Distance(anchor.position, transform.position) > activateDistance)
        {
            if (grabbable.isSelected)
            {
                interactionManager.CancelInteractableSelection(grabbable);
            }
            else
            {
                particles.Play();
                Debug.Log(message);
                transform.SetPositionAndRotation(anchor.position, anchor.rotation);
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
