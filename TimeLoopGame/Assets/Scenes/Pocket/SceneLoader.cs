using System;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using SM = UnityEngine.SceneManagement.SceneManager;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }


    public string CoreSceneName { get; private set; } = "Scenes/XRRig";
    public string CurrentSceneName { get; set; } = null;
    public bool IsLoading { get; private set; }
    public bool HasScene { get => CurrentSceneName != null; }

    private async void Start()
    {
        if (Instance != null) throw new InvalidOperationException("There should only be one SceneLoader");
        Instance = this;
        CoreSceneName = SM.GetActiveScene().name;
        await LoadScene("Scenes/PocketDimension");
    }

    public async Task LoadScene(string targetSceneName)
    {
        if (string.IsNullOrWhiteSpace(targetSceneName)) throw new ArgumentException("Scene name cannot be null or empty.");
        if (IsLoading) return;
        IsLoading = true;

        if (HasScene)
        {
            await SM.UnloadSceneAsync(CurrentSceneName);
        }
        CurrentSceneName = targetSceneName;
        await SM.LoadSceneAsync(targetSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        IsLoading = false;
    }
}
