using System;
using UnityEngine;

public class TeleportButton : MonoBehaviour
{
    public string ScenePath;
    public int GolemIndex = -1;

    public event EventHandler<TeleportArgs> teleportRequested;

    public void Teleport()
    {
        teleportRequested?.Invoke(this, new TeleportArgs(ScenePath, GolemIndex));
    }


    public class TeleportArgs : EventArgs
    {
        public string ScenePath { get; }
        public int GolemIndex { get; }

        public TeleportArgs(string scenePath, int golemIndex)
        {
            ScenePath = scenePath;
            GolemIndex = golemIndex;
        }
    }
}
