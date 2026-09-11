using System;
using UnityEngine;

public class GrabButtonScript : MonoBehaviour
{
    public float activateDistance = 0.2f;
    public string message;

    private Transform parentTransform;
    private bool activeLatch = false;

    private void Start()
    {
        parentTransform = GetComponentInParent<Transform>();
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(parentTransform.position, transform.position) > activateDistance && !activeLatch)
        {
            if (!activeLatch)
            {
                activeLatch = true;
                parentTransform.position = transform.position;
                Debug.Log(message);
            }
        }
        else
        {
            activeLatch = false;
        }
    }
}
