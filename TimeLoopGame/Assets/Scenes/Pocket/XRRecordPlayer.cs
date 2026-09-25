using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class XRRecordPlayer : MonoBehaviour
{
    public Transform head, rightHand, leftHand;
    public NPCInteractor rightInteractor, leftInteractor;

    private HandPlayer right, left;

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

    private void Start()
    {
        right = new HandPlayer(rightHand, rightInteractor);
        left = new HandPlayer(leftHand, leftInteractor);
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
        right.SetHandFrame(frame.RightHand);
        left.SetHandFrame(frame.LeftHand);
    }


    private class HandPlayer
    {
        private readonly Transform transform;
        private readonly NPCInteractor interactor;
        private bool isSelecting;

        public HandPlayer(Transform transform, NPCInteractor interactor)
        {
            this.transform = transform;
            this.interactor = interactor;
        }

        public void SetHandFrame(HandFrame frame)
        {
            transform.SetPositionAndRotation(frame.Pose.position, frame.Pose.rotation);
            if (frame.IsSelecting && !isSelecting)
            {
                interactor.Grab();
                isSelecting = true;
            }
            if (!frame.IsSelecting && isSelecting)
            {
                interactor.Release();
                isSelecting = false;
            }
        }
    }
}
