using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private PickUpObject _pickupPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromComponentInHierarchy().AsSingle();
        Container.Bind<InputService>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Inventory>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UseOfItems>().FromComponentInHierarchy().AsSingle(); 
        Container.Bind<Iteminfo>().FromComponentInHierarchy().AsSingle();
        Container.Bind<LocationManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<Respawn>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UiAnimatorScript>().FromComponentInHierarchy().AsSingle();
        Container.Bind<FPSCenter>().FromComponentInHierarchy().AsSingle();
    }
}
