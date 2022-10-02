using strange.extensions.command.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class SetTimeScaleCommand : Command
    {
        [Inject] public float TimeScale { get; set; }

        public override void Execute()
        {
            Time.timeScale = TimeScale;
        }
    }
}
