using System;
using DefaultNamespace;
using DefaultNamespace.Sphere;
using UnityEngine;
using Zenject;

public class ZigZagInstaller : MonoInstaller
{
    [Inject]
    Settings _settings = null;
    public override void InstallBindings()
    {
        InitAndBindPool<FieldPart, FieldPart.Pool>(_settings.FieldPartPool);
        InitAndBindPool<Crystal, Crystal.Pool>(_settings.CrystalPool);
        
        Container.Bind<FieldPart>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FieldGenerator>().AsSingle();
        Container.Bind<ScoreManager>().AsSingle();
        Container.Bind<GameDifficulty>().AsSingle();

        Container.Bind<CameraMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<KillZoneMovement>().FromComponentInHierarchy().AsSingle();
        GameSignalsInstaller.Install(Container);
    }

    private void InitAndBindPool<TItemContract, TPool>(PoolSettings poolSettings) where TPool : IMemoryPool
    {
        Container.BindMemoryPool<TItemContract, TPool>() 
          .WithInitialSize(poolSettings.InitialSize)
          .FromComponentInNewPrefab(poolSettings.Prefab)
          .UnderTransformGroup(poolSettings.TransformGroup);
    }

    [Serializable]
    public class Settings
    {
        public PoolSettings FieldPartPool;
        public PoolSettings CrystalPool;
    }
    [Serializable]
    public class PoolSettings
    {
        public GameObject Prefab;
        public string TransformGroup;
        public int InitialSize;
    }
}


