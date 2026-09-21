using UnityEngine;

public class RecordingManager : MonoBehaviour
{
    public GameObject myPrefab;

    private XRRecordCreator creator;
    bool recording = false;
    int frameCount = 0;

    void Start()
    {
        creator = FindAnyObjectByType<XRRecordCreator>();
        frameCount = 0;
    }

    public void StartRecording()
    {
        creator.StartRecording();
        recording = true;
        frameCount = 0;
        Debug.Log("start recording");
    }

    public void FixedUpdate()
    {
        if (recording)
        {
            frameCount++;
            Debug.Log("recording: " + frameCount);
            if (frameCount > 500)
            {
                creator.StopRecording();
                var obj = Instantiate(myPrefab, transform.position, transform.rotation);
                var record = obj.GetComponent<XRRecordPlayer>();
                record.SetFrameList(creator.Frames);
                record.Play();
                recording = false;
            }
        }
    }
}
