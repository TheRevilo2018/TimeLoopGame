using UnityEngine;

public class ExitScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Closes the built application
        Application.Quit();

        // Exits play mode inside the Unity Editor
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
