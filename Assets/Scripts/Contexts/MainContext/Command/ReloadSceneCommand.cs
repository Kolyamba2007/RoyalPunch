using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

public class ReloadSceneCommand : Command
{
    public override void Execute()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
