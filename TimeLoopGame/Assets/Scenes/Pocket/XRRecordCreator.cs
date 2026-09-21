using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class XRRecordCreator : MonoBehaviour
{
    public Transform head, rightHand, leftHand;

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

    private XRFrame getFrame()
    {
        return new XRFrame()
        {
            Head = new Pose(head.position, head.rotation),
            LeftHand = new Pose(leftHand.position, leftHand.rotation),
            RightHand = new Pose(rightHand.position, rightHand.rotation),
        };
    }
}
