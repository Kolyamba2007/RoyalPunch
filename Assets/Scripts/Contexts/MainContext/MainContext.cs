using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using UnityEngine;

namespace Contexts.MainContext
{
    public class MainContext : MVCSContext
    {
        public MainContext(MonoBehaviour view) : base(view)
        {
        }

        public MainContext(MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
        {
        }

        protected override void addCoreComponents()
        {
            base.addCoreComponents();

            injectionBinder
                .Unbind<ICommandBinder>();

            injectionBinder
                .Bind<ICommandBinder>()
                .To<SignalCommandBinder>()
                .ToSingleton();
        }

        protected override void mapBindings()
        {
            injectionBinder
                .Bind<GameConfig>()
                .To(GameConfig.Load())
                .ToSingleton();
        
            injectionBinder
                .Bind<Controls>()
                .To(new Controls())
                .ToSingleton();
        
            BindSignals();
            BindModels();
            BindViews();
            BindCommands();
            BindServices();
        }
    
        private void BindViews()
        {
            mediationBinder
                .Bind<CameraView>()
                .To<CameraMediator>();
        
            mediationBinder
                .Bind<PlayButtonView>()
                .To<PlayButtonMediator>();
        
            mediationBinder
                .Bind<PauseButtonView>()
                .To<PauseButtonMediator>();
        
            mediationBinder
                .Bind<PausePanelView>()
                .To<PausePanelMediator>();
        
            mediationBinder
                .Bind<EndGamePanelView>()
                .To<EndGamePanelMediator>();
        
            mediationBinder
                .Bind<JoystickView>()
                .To<JoystickMediator>();
        
            mediationBinder
                .Bind<BoxerView>()
                .To<BoxerMediator>();
        
            mediationBinder
                .Bind<BossView>()
                .To<BossMediator>();
        
            mediationBinder
                .Bind<MagneticEffectView>()
                .To<MagneticEffectMediator>();
        
            mediationBinder
                .Bind<ConfettiEffectView>()
                .To<ConfettiEffectMediator>();
        
            mediationBinder
                .Bind<AreaAttackView>()
                .To<AreaAttackMediator>();
        }
    
        private void BindSignals()
        {
            injectionBinder
                .Bind<StartCameraTransitionSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<StartGameSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<PauseGameSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<ContinueGameSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<EndGameSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<WinSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<LoseSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<ReloadSceneSignal>()
                .ToSingleton();

            injectionBinder
                .Bind<SetTimeScaleSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<SetMagneticEffectActiveSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<EnableAreaAttackSignal>()
                .ToSingleton();
        
            injectionBinder
                .Bind<EndAreaAttackSignal>()
                .ToSingleton();
        }
    
        private void BindCommands()
        {
            commandBinder
                .Bind<ReloadSceneSignal>()
                .To<ReloadSceneCommand>();
        
            commandBinder
                .Bind<SetTimeScaleSignal>()
                .To<SetTimeScaleCommand>();
        }

        private void BindModels()
        {
            injectionBinder
                .Bind<IUnitState>()
                .To<UnitState>()
                .ToSingleton();
        }

        private void BindServices()
        {
            injectionBinder
                .Bind<IUnitService>()
                .To<UnitService>()
                .ToSingleton();
        }
    }
}
