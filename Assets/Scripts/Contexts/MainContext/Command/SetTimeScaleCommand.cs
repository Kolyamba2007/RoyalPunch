using strange.extensions.command.impl;
using UnityEngine;

public class SetTimeScaleCommand : Command
{
    [Inject] public float TimeScale { get; set; }

    public override void Execute()
    {
        Time.timeScale = TimeScale;
    }
}
