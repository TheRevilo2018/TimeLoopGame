using System;
using System.Threading.Tasks;
using UnityEngine;
using SM = UnityEngine.SceneManagement.SceneManager;

public class SceneLoader
{
    public string CoreSceneName { get; private set; } = "Scenes/XRRig";
    public string CurrentSceneName { get; set; } = null;
    public bool IsLoading { get; private set; }
    public bool HasScene { get => CurrentSceneName != null; }

    public SceneLoader()
    {
        CoreSceneName = SM.GetActiveScene().name;
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
        SM.SetActiveScene(SM.GetSceneByName(targetSceneName));
        IsLoading = false;
    }
}
