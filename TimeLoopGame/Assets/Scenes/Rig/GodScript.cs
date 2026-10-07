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

    public void Start()
    {
        loader = new SceneLoader();
        characterManger.RecordingFinished += CharacterManger_RecordingFinished;
        startLoadHome();
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
            if (home)
            {
                buttonList = FindObjectsByType<TeleportButton>().ToList();
                foreach (var button in buttonList)
                {
                    button.teleportRequested += Button_teleportRequested;
                }
            }
            else
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

        foreach (var button in buttonList)
        {
            button.teleportRequested -= Button_teleportRequested;
        }

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
