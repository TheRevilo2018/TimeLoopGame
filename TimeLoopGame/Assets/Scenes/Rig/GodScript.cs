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

    public async void Start()
    {
        loader = new SceneLoader();
        await loader.LoadScene(MAIN_SCENE_PATH);
        buttonList = FindObjectsByType<TeleportButton>().ToList();

        foreach (var button in buttonList)
        {
            button.teleportRequested += Button_teleportRequested;
        }
    }

    private void Button_teleportRequested(object sender, TeleportButton.TeleportArgs e)
    {
        if (!startLoad(MAIN_SCENE_PATH)) return;
        golemId = e.GolemIndex;
    }

    private void Update()
    {
        if (loadingTask != null && loadingTask.IsCompleted)
        {
            loadingTask = null;
            characterManger.StartScene();
            var anchor = characterManger.RecordAnchor(golemId);
            player.transform.SetPositionAndRotation(anchor.transform.position, anchor.transform.rotation);
            characterManger.PlayRecordings(0, 1000);
        }

        if (!characterManger.Running)
        {
            startLoad(MAIN_SCENE_PATH);
        }
    }

    private bool startLoad(string scenePath)
    {
        if (loadingTask == null) return false;
        loadingTask = loader.LoadScene(scenePath);
        return true;
    }
}
