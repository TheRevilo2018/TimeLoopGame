using System;
using System.Collections.Generic;
using UnityEngine;

public class XRRecordPlayer : MonoBehaviour
{
    public Transform head, rightHand, leftHand;
    public XRInputDeviceButtonRecorded rightSelect, rightActivate, leftSelect, leftActivate;
    private HandPlayer right, left;

    private bool running;
    private IReadOnlyList<XRFrame> frameList;
    private int frameIndex = 0;

    public void SetFrameList(IReadOnlyList<XRFrame> frameList)
    {
        if (running) throw new InvalidOperationException("Cannot change the script while it's running");

        this.frameList = frameList;
    }

    public void PlayRecording()
    {
        running = true;
    }

    public void PauseRecording()
    {
        running = false;
    }

    public void ResetRecording()
    {
        frameIndex = 0;
    }

    private void Start()
    {
        right = new HandPlayer(rightHand, rightSelect, rightActivate);
        left = new HandPlayer(leftHand, leftSelect, leftActivate);
    }

    private void FixedUpdate()
    {
        if (running)
        {
            if (frameIndex < frameList.Count)
            {
                setFrame(frameList[frameIndex]);
                frameIndex++;
            }
            else
            {
                PauseRecording();
            }
        }
    }

    private void setFrame(XRFrame frame)
    {
        head.SetPositionAndRotation(frame.Head.position, frame.Head.rotation);
        right.SetHandFrame(frame.RightHand);
        left.SetHandFrame(frame.LeftHand);
    }


    private class HandPlayer
    {
        private readonly Transform transform;
        private readonly XRInputDeviceButtonRecorded selectButton;
        private readonly XRInputDeviceButtonRecorded activateButton;

        public HandPlayer(Transform transform, XRInputDeviceButtonRecorded selectButton, XRInputDeviceButtonRecorded activateButton)
        {
            this.transform = transform;
            this.selectButton = selectButton;
            this.activateButton = activateButton;
        }

        public void SetHandFrame(HandFrame frame)
        {
            selectButton.SimulatedButtonValue = frame.IsSelecting ? 1 : 0;
            activateButton.SimulatedButtonValue = frame.IsActivating ? 1 : 0;
            transform.SetPositionAndRotation(frame.Pose.position, frame.Pose.rotation);
        }
    }
}
