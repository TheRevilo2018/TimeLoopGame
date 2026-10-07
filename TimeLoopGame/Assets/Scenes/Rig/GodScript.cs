using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class GodScript : MonoBehaviour
{
    public Transform player;
    public RecordedCharacterManger characterManger;

    private SceneLoader loader;
    private List<TeleportButton> buttonList;
    private Task loadingTask;
    private int golemId;
    private const string MAIN_SCENE_PATH = "Scenes/PocketDimension";
    private bool home = true;

    public async void Start()
    {
        loader = new SceneLoader();
        await loader.LoadScene(MAIN_SCENE_PATH);
        buttonList = FindObjectsByType<TeleportButton>().ToList();
        characterManger.RecordingFinished += CharacterManger_RecordingFinished;

        foreach (var button in buttonList)
        {
            button.teleportRequested += Button_teleportRequested;
        }
    }

    private void CharacterManger_RecordingFinished(object sender, System.EventArgs e)
    {
        startLoadHome();
    }

    private void Button_teleportRequested(object sender, TeleportButton.TeleportArgs e)
    {
        if (!startLoad(e.ScenePath)) return;
        golemId = e.GolemIndex;
    }

    private void Update()
    {
        if (loadingTask != null && loadingTask.IsCompleted)
        {
            loadingTask = null;
            if (!home)
            {
                characterManger.StartScene();
                var anchor = characterManger.RecordAnchor(golemId);
                player.transform.SetPositionAndRotation(anchor.transform.position, anchor.transform.rotation);
                characterManger.PlayRecordings(0, 1000);
            }
        }
    }

    private bool startLoad(string scenePath)
    {
        if (loadingTask != null) return false;
        home = false;
        loadingTask = loader.LoadScene(scenePath);
        return true;
    }

    private bool startLoadHome()
    {
        if (loadingTask != null) return false;
        home = true;
        loadingTask = loader.LoadScene(MAIN_SCENE_PATH);
        return true;
    }
}
