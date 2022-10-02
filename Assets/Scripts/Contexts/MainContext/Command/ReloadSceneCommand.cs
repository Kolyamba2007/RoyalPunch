using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

namespace Contexts.MainContext
{
    public class ReloadSceneCommand : Command
    {
        public override void Execute()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
