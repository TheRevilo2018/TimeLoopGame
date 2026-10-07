using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecordedCharacterManger : MonoBehaviour
{
    XRRecordCreator creator;
    RecordedCharacterAnchor[] anchors;
    readonly Dictionary<int, IReadOnlyList<XRFrame>> frames = new Dictionary<int, IReadOnlyList<XRFrame>>();
    int anchorId;
    int startingFrame, endingFrame, currentFrame;
    
    public bool Running { get; private set; } = false;

    void Start()
    {
        creator = FindAnyObjectByType<XRRecordCreator>();
    }

    public void FixedUpdate()
    {
        if (Running)
        {
            currentFrame++;
            if (currentFrame > endingFrame)
            {
                creator.StopRecording();
                frames[anchorId] = creator.Frames;
                Running = false;
            }
        }
    }

    public void StartScene()
    {
        anchors = FindObjectsByType<RecordedCharacterAnchor>();
        foreach (var anchor in anchors)
        {
            if (frames.ContainsKey(anchor.AnchorId))
            {
                anchor.player.SetFrameList(frames[anchor.AnchorId]);
            }
        }
    }

    public RecordedCharacterAnchor RecordAnchor(int anchorId)
    {
        var target = anchors.SingleOrDefault(x => x.AnchorId == anchorId);
        if (target == null) throw new KeyNotFoundException("An anchor with ID " + anchorId + " cannot be found.");
        this.anchorId = anchorId;

        target.gameObject.SetActive(false);
        return target;
    }

    public void PlayRecordings(int startingFrame, int endingFrame)
    {
        this.startingFrame = startingFrame;
        this.endingFrame = endingFrame;
        currentFrame = startingFrame;
        creator.StartRecording();
        Running = true;
        foreach (var anchor in anchors)
        {
            if (anchor.isActiveAndEnabled)
            {
                anchor.player.PlayRecording();
            }
        }
    }
}
