using System;
using System.Collections.Generic;
using UnityEngine;

public class XRRecordPlayer : MonoBehaviour
{
    public Transform head, rightHand, leftHand;

    private bool running;
    private IReadOnlyList<XRFrame> frameList;
    private int frameIndex = 0;

    public void SetFrameList(IReadOnlyList<XRFrame> frameList)
    {
        if (running) throw new InvalidOperationException("Cannot change the script while it's running");

        this.frameList = frameList;
    }

    public void Play()
    {
        running = true;
        frameIndex = 0;
    }

    public void Stop()
    {
        running = false;
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
                Stop();
            }
        }
    }

    private void setFrame(XRFrame frame)
    {
        head.SetPositionAndRotation(frame.Head.position, frame.Head.rotation);
        rightHand.SetPositionAndRotation(frame.RightHand.position, frame.RightHand.rotation);
        leftHand.SetPositionAndRotation(frame.LeftHand.position, frame.LeftHand.rotation);
    }
}
