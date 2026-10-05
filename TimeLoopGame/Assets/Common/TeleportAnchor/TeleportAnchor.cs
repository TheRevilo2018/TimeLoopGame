using UnityEngine;
using System;
using System.Threading.Tasks;

public class TeleportAnchor : MonoBehaviour
{
    public string SceneAddress;

    private Task teleportTask;

    private void Start()
    {
        if (string.IsNullOrWhiteSpace(SceneAddress)) throw new ArgumentException(nameof(SceneAddress) + " not set.");
    }

    public void Teleport()
    {
        if (teleportTask != null) return;
        teleportTask = SceneLoader.Instance.LoadScene(SceneAddress);
    }
}
