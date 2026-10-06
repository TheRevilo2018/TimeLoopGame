using UnityEngine;

public class XRFrame
{
    public Pose Head;
    public HandFrame LeftHand;
    public HandFrame RightHand;
}

public class HandFrame
{
    public Pose Pose;
    public bool IsSelecting;
    public bool IsActivating;
}
