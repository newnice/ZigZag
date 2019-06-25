using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "ZigZag/Configuration")]
public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
{
    public ZigZagInstaller.Settings MainInstaller;
    public FieldGenerator.Settings FieldGenerator;
    public MoveState.Settings MoveSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(MainInstaller).IfNotBound();
        Container.BindInstance(FieldGenerator).IfNotBound();
        Container.BindInstance(MoveSettings).IfNotBound();
    }
}