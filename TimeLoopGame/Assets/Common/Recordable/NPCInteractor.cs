using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class NPCInteractor : XRBaseInteractor
{
    private List<Collider> grabbables = new();

    public void Grab()
    {
        var interactable = grabbables
            .Select(x => x.GetComponent<IXRSelectInteractable>())
            .FirstOrDefault();
        if (interactable == null) return;
        StartManualInteraction(interactable);
    }

    public void Release()
    {
        EndManualInteraction();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    public void FixedUpdate()
    {
        grabbables.RemoveAll(item => item == null || !item.enabled || !item.gameObject.activeInHierarchy);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!grabbables.Contains(other))
        {
            grabbables.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (grabbables.Contains(other))
        {
            grabbables.Remove(other);
        }
    }

    // Required overrides, but can be no-ops if you only use manual interaction
    public override bool CanSelect(IXRSelectInteractable interactable) => true;
}