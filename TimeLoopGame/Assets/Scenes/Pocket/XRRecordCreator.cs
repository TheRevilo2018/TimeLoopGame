using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRRecordCreator : MonoBehaviour
{
    public Transform head, rightHand, leftHand;
    public InputActionReference rightSelect, rightActivate, leftSelect, leftActivate; 

    private bool recording;
    private List<XRFrame> frameList;

    public IReadOnlyList<XRFrame> Frames => frameList;

    public void StartRecording()
    {
        recording = true;
        frameList = new List<XRFrame>();
    }

    public void StopRecording()
    {
        recording = false;
    }

    private void FixedUpdate()
    {
        if (recording)
        {
            frameList.Add(getFrame());
        }
    }

    private void OnEnable()
    {
        leftSelect.action.Enable();
        leftActivate.action.Enable();
        rightSelect.action.Enable();
        rightActivate.action.Enable();
    }

    private void OnDisable()
    {
        leftSelect.action.Disable();
        leftActivate.action.Disable();
        rightSelect.action.Disable();
        rightActivate.action.Disable();
    }

    private XRFrame getFrame()
    {
        return new XRFrame()
        {
            Head = new Pose(head.position, head.rotation),
            LeftHand = getHandFrame(leftHand, leftSelect, leftActivate),
            RightHand = getHandFrame(rightHand, rightSelect, rightActivate),
        };
    }

    private HandFrame getHandFrame(Transform pose, InputActionReference select, InputActionReference activate)
    {
        return new HandFrame
        {
            Pose = new Pose(pose.position, pose.rotation),
            IsSelecting = select.action.ReadValue<bool>(),
            IsActivating = activate.action.ReadValue<bool>(),
        };
    }
}
