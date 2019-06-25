using System;
using UnityEngine;
using Zenject;

public class SphereInstaller : MonoInstaller
{
    [SerializeField]
    private Settings settings = null;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StateManager>().AsSingle();
        
        Container.Bind<MoveState>().AsSingle().WithArguments(settings.SphereRigidbody);
        Container.Bind<StayState>().AsSingle().WithArguments(settings.SphereRigidbody);
        Container.Bind<FallingState>().AsSingle();
    }

    [Serializable]
    public class Settings
    {
        public Rigidbody SphereRigidbody;
    }

}