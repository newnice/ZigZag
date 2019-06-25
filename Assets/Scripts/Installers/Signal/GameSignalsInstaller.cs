using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PositionChangedSignal>();

        Container.BindSignal<PositionChangedSignal>()
            .ToMethod<KillZoneMovement>((x, s) => x.MoveKillZonePosition(s.Position)).FromResolve();
        Container.BindSignal<PositionChangedSignal>()
            .ToMethod<CameraMovement>((x, s) => x.StartCameraMovement(s.Position)).FromResolve();
        Container.BindSignal<PositionChangedSignal>()
            .ToMethod<FieldGenerator>((x, s) => x.OnVisibleFieldChanged(s.Position)).FromResolve();
    }
}
